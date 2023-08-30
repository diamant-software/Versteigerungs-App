// DeviceGroupsController.cs
using Microsoft.AspNetCore.Mvc;
using Versteigerungs_App.Models;
using Versteigerungs_App.Services;

namespace DeviceAuctionAPI.Controllers;

[Route("api/device-groups")]
[ApiController]
public class DeviceGroupsController : ControllerBase
{
    private readonly IRepository _repository;

    public DeviceGroupsController(IRepository repository)
    {
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> CreateDeviceGroup(DeviceGroup deviceGroup)
    {
        deviceGroup.Id = Guid.NewGuid();
        await _repository.CreateAsync(deviceGroup);
        return CreatedAtAction(nameof(GetDeviceGroup), new { id = deviceGroup.Id }, deviceGroup);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDeviceGroup(Guid id)
    {
        var deviceGroup = await _repository.GetByIdAsync(id);
        if (deviceGroup == null)
        {
            return NotFound();
        }
        return Ok(deviceGroup);
    }
}