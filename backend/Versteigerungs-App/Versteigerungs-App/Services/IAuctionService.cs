

using Versteigerungs_App.Models;

namespace Versteigerungs_App.Services
{
    public interface IAuctionService
    {
        Task<bool> SetAuctionTimes(DateTime startTime, DateTime endTime);
        Task<AuctionTime?> GetAuctionTimes();
    }

    public class AuctionService : IAuctionService
    {
        private readonly IAuctionRepository _auctionRepository;

        public AuctionService(IAuctionRepository auctionRepository)
        {
            _auctionRepository = auctionRepository;
        }

        public async Task<bool> SetAuctionTimes(DateTime startTime, DateTime endTime)
        {
            try
            {
                var auctionTimes = new AuctionTime
                {
                    StartTime = startTime,
                    EndTime = endTime
                };
                
                // auction must be at least one day long
                if (endTime < startTime.AddDays(1))
                {
                    return false;
                }

                await _auctionRepository.SetAuctionTimes(auctionTimes);
                return true;
            }
            catch (Exception)
            {
                // Log the exception
                return false;
            }
        }

        public async Task<AuctionTime?> GetAuctionTimes()
        {
            return await _auctionRepository.GetAuctionTimes();
        }
    }
}