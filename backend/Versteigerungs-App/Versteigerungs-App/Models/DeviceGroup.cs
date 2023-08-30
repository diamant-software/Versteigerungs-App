namespace Versteigerungs_App.Models;


public class DeviceGroup
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    
    public required IEnumerable<Device> Devices { get; set; }
}

public class Device
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Model { get; set; }
    public required decimal Price { get; set; }
    public required string SerialNumber { get; set; }
    public User? CurrentHighestBidder { get; set; }
}

public class User
{
    public required string Username { get; set; }
}