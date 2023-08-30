
namespace Versteigerungs_App.Models;

public class PersistedBid
{
    public required DateTime TimestampUtc{get;set;} 
    public required Guid DeviceId {get;set;}
    
    public required string Username{get;set;}
    public required decimal Price {get;set;}
}