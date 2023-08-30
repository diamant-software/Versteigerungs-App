using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Versteigerungs_App.Models;
using Versteigerungs_App.Services;
using Versteigerungs_App.Utils;

namespace Versteigerungs_App.Controllers
{
    [Route("api/bid")]
    [ApiController]
    [Authorize]
    public class BiddingController : ControllerBase
    {
        private readonly IBiddingService _biddingService;

        public BiddingController(IBiddingService biddingService)
        {
            _biddingService = biddingService;
        }

        [HttpPatch("{deviceId}")]
        public async Task<IActionResult> PlaceBid(Guid deviceId, [FromBody] Bid bid)
        {
            try
            {
                var user = User.GetUser();
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