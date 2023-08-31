using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Versteigerungs_App.Models;

namespace Versteigerungs_App.Services
{
    public interface IAuctionRepository
    {
        Task SetAuctionTimes(AuctionTime auctionTime);
        Task<AuctionTime?> GetAuctionTimes();
    }

    public class AuctionRepository : IAuctionRepository
    {
        private readonly IMongoCollection<Auction> _auctionTimeCollection;

        public AuctionRepository(IMongoDbCollectionFactory<Auction> mongoDbDbCollectionFactory,
            IOptionsMonitor<DatabaseSettings> optionsMonitor)
        {
            var settings = optionsMonitor.Get("auction");
            _auctionTimeCollection = mongoDbDbCollectionFactory.GetCollection(settings);
        }

        // we only have one auction object in the collection which we either
        // 1. create (if it does not exist yet)
        // 2. update (if it already exists)
        public async Task SetAuctionTimes(AuctionTime auctionTime)
        {
            using var cursor = await _auctionTimeCollection.FindAsync(auc => true);
            var first =  cursor.FirstOrDefault();

            if (first == null)
            {
                first = new Auction
                {
                    AuctionTime = auctionTime,
                    Id = Guid.NewGuid()
                };
            }
            else
            {
                first.AuctionTime = auctionTime;
            }

            await _auctionTimeCollection.DeleteManyAsync(_ => true);
            await _auctionTimeCollection.InsertOneAsync(first);
        }

        public async Task<AuctionTime?> GetAuctionTimes()
        {
            using var cursor = await _auctionTimeCollection.FindAsync(auc => true);
            return cursor.FirstOrDefault()?.AuctionTime;
        }
    }
}