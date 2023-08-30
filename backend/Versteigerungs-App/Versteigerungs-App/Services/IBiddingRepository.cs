using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Versteigerungs_App.Models;

namespace Versteigerungs_App.Services
{
    public interface IBiddingRepository
    {
        Task<IEnumerable<PersistedBid>> GetBidsByUserAsync(string userId);
        Task<PersistedBid> GetBidByUserAndDeviceAsync(string userId, Guid deviceId);
        Task<bool> PlaceBidAsync(string userId, Guid deviceId, decimal price);
        Task<bool> UpdateBidAsync(string userId, Guid deviceId, decimal price);
    }

    public class BiddingRepository : IBiddingRepository
    {
        private readonly IMongoCollection<PersistedBid> _bidCollection;

        public BiddingRepository(IMongoDbCollectionFactory<PersistedBid> mongoDbDbCollectionFactory,
            IOptionsMonitor<DatabaseSettings> optionsMonitor)
        {
            var settings = optionsMonitor.Get("devices");
            _bidCollection = mongoDbDbCollectionFactory.GetCollection(settings);
        }

        public async Task<IEnumerable<PersistedBid>> GetBidsByUserAsync(string userId)
        {
            var filter = Builders<PersistedBid>.Filter.Eq(b => b.Username, userId);
            return await _bidCollection.Find(filter).ToListAsync();
        }

        public async Task<PersistedBid> GetBidByUserAndDeviceAsync(string userId, Guid deviceId)
        {
            var filter = Builders<PersistedBid>.Filter.Eq(b => b.Username, userId) & Builders<PersistedBid>.Filter.Eq(b => b.DeviceId, deviceId);
            return await _bidCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<bool> PlaceBidAsync(string userId, Guid deviceId, decimal price)
        {
            var bid = new PersistedBid
            {
                TimestampUtc = DateTime.UtcNow,
                DeviceId = deviceId,
                Username = userId,
                Price = price
            };

            await _bidCollection.InsertOneAsync(bid);
            return true;
        }

        public async Task<bool> UpdateBidAsync(string userId, Guid deviceId, decimal price)
        {
            var filter = Builders<PersistedBid>.Filter.Eq(b => b.Username, userId) & Builders<PersistedBid>.Filter.Eq(b => b.DeviceId, deviceId);
            var update = Builders<PersistedBid>.Update.Set(b => b.Price, price).Set(b => b.TimestampUtc, DateTime.UtcNow);
            var updateResult = await _bidCollection.UpdateOneAsync(filter, update);

            return updateResult.ModifiedCount > 0;
        }
    }
}
