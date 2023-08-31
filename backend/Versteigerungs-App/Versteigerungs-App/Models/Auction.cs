namespace Versteigerungs_App.Models;

public class Auction
{
    public required Guid Id { get; set; }
    public required AuctionTime AuctionTime { get; set; }
}