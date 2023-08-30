using System;
using System.Threading.Tasks;
using Versteigerungs_App.Models;

namespace Versteigerungs_App.Services
{
    public interface IDeviceService
    {
        Task<Device> CreateDevice(Guid groupId, Device device);
        Task<Device> UpdateDevice(Guid groupId, Guid deviceId, Device device);
        Task<Device> GetDevice(Guid groupId, Guid deviceId);
    }

    public class DeviceService : IDeviceService
    {
        private readonly IDevicesRepository _deviceGroupDevicesRepository;

        public DeviceService(IDevicesRepository deviceGroupDevicesRepository)
        {
            _deviceGroupDevicesRepository = deviceGroupDevicesRepository;
        }

        public async Task<Device> CreateDevice(Guid groupId, Device device)
        {
            var group = await _deviceGroupDevicesRepository.GetByIdAsync(groupId);
            if (group == null)
            {
                throw new Exception();
            }

            if (group.Devices.Any(d => d.Id == device.Id))
            {
                throw new ArgumentException("device already exists");
            }

            var groupAltered = group.Devices.ToList();
            groupAltered.Add(device);
            group.Devices = groupAltered;
            return device;
        }

        public async Task<Device> UpdateDevice(Guid groupId, Guid deviceId, Device device)
        {
            var group = await _deviceGroupDevicesRepository.GetByIdAsync(groupId);
            if (group == null)
            {
                throw new Exception();
            }

            var groupAltered = group.Devices.Where(d => d.Id != device.Id).ToList();

            groupAltered.Add(device);
            group.Devices = groupAltered;

            return device;
        }

        public Task<Device> GetDevice(Guid groupId, Guid deviceId)
        {
            throw new NotImplementedException();
        }
    }
}