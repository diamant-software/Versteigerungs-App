using Microsoft.AspNetCore.Mvc;
using Versteigerungs_App.Models;
using Versteigerungs_App.Services;

namespace Versteigerungs_App.Controllers
{
    [Route("api/bid")]
    [ApiController]
    public class BiddingController : ControllerBase
    {
        private readonly IBiddingService _biddingService;

        public BiddingController(IBiddingService biddingService)
        {
            _biddingService = biddingService;
        }

        [HttpPost("{deviceId}")]
        public async Task<IActionResult> PlaceBid(Guid deviceId, [FromBody] Bid bid)
        {
            try
            {
                var user = new User { Username = "TestUser" };
                var success = await _biddingService.PlaceBid(deviceId, bid, user);
                if (success)
                {
                    return Ok("Bid placed successfully.");
                }
                else
                {
                    return BadRequest("Bid could not be placed.");
                }
            }
            catch (Exception)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}