using Microsoft.Extensions.Options;
using Versteigerungs_App.Models;
using MongoDB.Driver;

namespace Versteigerungs_App.Services;

public interface IDevicesRepository
{
    Task<IEnumerable<DeviceGroup>> GetAllAsync();
    Task<DeviceGroup?> GetByIdAsync(Guid id);
    Task<DeviceGroup> CreateAsync(DeviceGroup entity);
    Task UpdateAsync(DeviceGroup group);
}

public class MongoDevicesRepository : IDevicesRepository
{
    private readonly IMongoCollection<DeviceGroup> _collection;

    public MongoDevicesRepository(IMongoDbCollectionFactory<DeviceGroup> mongoDbDbCollectionFactory,
        IOptionsMonitor<DatabaseSettings> optionsMonitor)
    {
        var settings = optionsMonitor.Get("devices");
        _collection = mongoDbDbCollectionFactory.GetCollection(settings);
    }

    public async Task<IEnumerable<DeviceGroup>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<DeviceGroup?> GetByIdAsync(Guid id)
    {
        return await _collection.Find(entity => entity.Id == id).FirstOrDefaultAsync();
    }

    public async Task<DeviceGroup> CreateAsync(DeviceGroup entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(DeviceGroup group)
    {
        await _collection.ReplaceOneAsync(entity => entity.Id == group.Id, group);
    }
}