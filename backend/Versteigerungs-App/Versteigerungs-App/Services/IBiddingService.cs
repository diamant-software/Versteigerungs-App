using Versteigerungs_App.Models;

namespace Versteigerungs_App.Services;

public interface IBiddingService
{
    Task<bool> PlaceBid(Guid deviceId, Bid bid, User user);
}

public class BiddingService : IBiddingService
{
    private readonly IDevicesRepository _deviceGroupDevicesRepository;
    private readonly IBiddingRepository _biddingRepository;
    private readonly ILogger<BiddingService> _logger;

    public BiddingService(IDevicesRepository deviceGroupDevicesRepository, ILogger<BiddingService> logger, IBiddingRepository biddingRepository)
    {
        _deviceGroupDevicesRepository = deviceGroupDevicesRepository;
        _logger = logger;
        _biddingRepository = biddingRepository;
    }

    public async Task<bool> PlaceBid(Guid deviceId, Bid bid, User user)
    {
        try
        {
            if (! await UserCanPlaceBid(user, deviceId))
            {
                return false;
            }
            
            var deviceGroups = await _deviceGroupDevicesRepository.GetAllAsync();
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

            await _deviceGroupDevicesRepository.UpdateAsync(deviceGroup);

            await _biddingRepository.PlaceBidAsync(user.Username, deviceId, bid.Price);

            return true;

        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while placing bid");
            return false;
        }
    }

    private async Task<bool> UserCanPlaceBid(User user, Guid deviceId)
    {
        var userBids = await _biddingRepository.GetBidsByUserAsync(user.Username);
        var userBidsForDevice = userBids.Where(bid => bid.DeviceId == deviceId);
        return userBidsForDevice.All(bid => bid.TimestampUtc.DayOfYear < DateTime.UtcNow.DayOfYear);
    }
}