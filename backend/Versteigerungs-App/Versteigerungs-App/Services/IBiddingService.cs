using Versteigerungs_App.Models;

namespace Versteigerungs_App.Services;

public interface IBiddingService
{
    Task<bool> PlaceBid(Guid deviceId, Bid bid, User user);
}

public class BiddingService : IBiddingService
{
    private readonly IRepository _deviceGroupRepository;

    public BiddingService(IRepository deviceGroupRepository)
    {
        _deviceGroupRepository = deviceGroupRepository;
    }

    public async Task<bool> PlaceBid(Guid deviceId, Bid bid, User user)
    {
        // Implement your bidding logic here
        try
        {
            var deviceGroups = await _deviceGroupRepository.GetAllAsync();
            var deviceGroup = deviceGroups.FirstOrDefault(group => group.Devices.Any(device => device.Id == deviceId));

            if (deviceGroup == null)
            {
                return false;
            }

            var device = deviceGroup.Devices.First(d => d.Id == deviceId);
            
            if (device.Price >= bid.Price) return false;
            
            device.Price = bid.Price;
            device.CurrentHighestBidder = new User { Username = user.Username };

            var groupAltered = deviceGroup.Devices.Where(d => d.Id != deviceId).ToList();
            groupAltered.Add(device);
            deviceGroup.Devices = groupAltered;

            await _deviceGroupRepository.UpdateAsync(deviceGroup);

            return true;

        }
        catch (Exception)
        {
            // Log the exception
            return false;
        }
    }
}