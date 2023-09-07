using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Versteigerungs_App.Models;
using Versteigerungs_App.Services;
using Versteigerungs_App.Utils;

namespace DeviceAuctionAPI.Controllers;

[Route("api/device-groups")]
[ApiController]
[Authorize]
public class DeviceGroupsController : ControllerBase
{
    private readonly IDevicesRepository _devicesRepository;

    public DeviceGroupsController(IDevicesRepository devicesRepository)
    {
        _devicesRepository = devicesRepository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDeviceGroup(DeviceGroup deviceGroup)
    {
        if (!User.GetUser().IsAdmin()) return Forbid();
        
        deviceGroup.Id = Guid.NewGuid();
        await _devicesRepository.CreateAsync(deviceGroup);
        return CreatedAtAction(nameof(GetDeviceGroup), new { id = deviceGroup.Id }, deviceGroup);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeviceGroup(Guid id)
    {
        var deviceGroup = await _devicesRepository.GetByIdAsync(id);
        if (deviceGroup == null)
        {
            return NotFound();
        }
        return Ok(deviceGroup);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllDeviceGroups()
    {
        var deviceGroups = await _devicesRepository.GetAllAsync();
        return Ok(deviceGroups);
    }
}