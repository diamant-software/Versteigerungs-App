using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web.Resource;
using Versteigerungs_App.Models;
using Versteigerungs_App.Services;
using Versteigerungs_App.Utils;

namespace Versteigerungs_App.Controllers
{
    [Route("api/auction")]
    [ApiController]
    [Authorize]
    [RequiredScope("unrestricted")]
    public class AuctionController : ControllerBase
    {
        private readonly IAuctionService _auctionService;
        private readonly ILogger<AuctionController> _logger;

        public AuctionController(IAuctionService auctionService, ILogger<AuctionController> logger)
        {
            _auctionService = auctionService;
            _logger = logger;
        }

        [HttpPatch("times")]
        public async Task<IActionResult> SetAuctionTimes([FromBody] AuctionTime auctionTimes)
        {
            if (!User.GetUser().IsAdmin()) return Forbid();

            try
            {
                var success = await _auctionService.SetAuctionTimes(auctionTimes.StartTime, auctionTimes.EndTime);
                if (success)
                {
                    return Ok("Auction times set successfully.");
                }

                return BadRequest("Auction times could not be set.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
        
        [HttpGet("times")]
        public async Task<IActionResult> GetAuctionTimes()
        {
            try
            {
                var auctionTimes = await _auctionService.GetAuctionTimes();
                return Ok(auctionTimes);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request.");
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}