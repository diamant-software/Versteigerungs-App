namespace Versteigerungs_App.Models;


public class DeviceGroup
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public IEnumerable<Device> Devices { get; set; }
}

public class Device
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public decimal Price { get; set; }
    public string SerialNumber { get; set; }
}