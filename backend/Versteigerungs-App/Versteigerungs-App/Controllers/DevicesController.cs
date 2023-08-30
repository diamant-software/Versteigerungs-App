using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Versteigerungs_App.Models;
using Versteigerungs_App.Services;
using Versteigerungs_App.Utils;

namespace Versteigerungs_App.Controllers
{
    [Route("api/device-groups/{groupId}/devices")]
    [ApiController]
    [Authorize]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DevicesController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDevice(Guid groupId, [FromBody] Device device)
        {
            if (!User.GetUser().IsAdmin()) return Forbid();

            try
            {
                var createdDevice = await _deviceService.CreateDevice(groupId, device);
                if (createdDevice != null)
                {
                    return CreatedAtAction(nameof(GetDevice), new { groupId, deviceId = createdDevice.Id }, createdDevice);
                }
                else
                {
                    return BadRequest("Device could not be created.");
                }
            }
            catch (Exception )
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpPatch("{deviceId}")]
        public async Task<IActionResult> UpdateDevice(Guid groupId, Guid deviceId, [FromBody] Device device)
        {
            if (!User.GetUser().IsAdmin()) return Forbid();
            try
            {
                var updatedDevice = await _deviceService.UpdateDevice(groupId, deviceId, device);
                if (updatedDevice != null)
                {
                    return Ok(updatedDevice);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception )
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }

        [HttpGet("{deviceId}")]
        public async Task<IActionResult> GetDevice(Guid groupId, Guid deviceId)
        {
            try
            {
                var device = await _deviceService.GetDevice(groupId, deviceId);
                if (device != null)
                {
                    return Ok(device);
                }
                else
                {
                    return NotFound();
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
