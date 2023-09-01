using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Versteigerungs_App.Models;
using Versteigerungs_App.Services;

public class AuctionFunction
{

    private readonly IAuctionService _auctionService;
    private readonly ILogger<AuctionFunction> _logger;

    public AuctionFunction(IAuctionService auctionService, ILogger<AuctionFunction> logger)
    {
        _auctionService = auctionService;
        _logger = logger;
    }
    
    [Authorize]
    [FunctionName("SetAuctionTimes")]
    public async Task<IActionResult> SetAuctionTimes(
        [HttpTrigger(AuthorizationLevel.Function, "patch", Route = "auction/times")] HttpRequest req)
    {
        try
        {
            // Validate admin access here (you'll need to implement this logic)
            // if (!User.GetUser().IsAdmin())
            // {
            //     return new UnauthorizedResult();
            // }

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var auctionTimes = JsonSerializer.Deserialize<AuctionTime>(requestBody) ?? throw new ArgumentException("Invalid request body.");

            var success = await _auctionService.SetAuctionTimes(auctionTimes.StartTime, auctionTimes.EndTime);
            if (success)
            {
                return new OkObjectResult("Auction times set successfully.");
            }

            return new BadRequestObjectResult("Auction times could not be set.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request.");
            return new StatusCodeResult(500);
        }
    }

    [FunctionName("GetAuctionTimes")]
    public async Task<IActionResult> GetAuctionTimes(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "auction/times")] HttpRequest req)
    {
        try
        {
            var auctionTimes = await _auctionService.GetAuctionTimes();
            return new OkObjectResult(auctionTimes);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while processing the request.");
            return new StatusCodeResult(500);
        }
    }
}
