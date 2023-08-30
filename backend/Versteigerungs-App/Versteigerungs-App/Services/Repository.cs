using Versteigerungs_App.Models;

namespace Versteigerungs_App.Services;

using MongoDB.Driver;


// IRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository
{
    Task<IEnumerable<DeviceGroup>> GetAllAsync();
    Task<DeviceGroup> GetByIdAsync(Guid id);
    Task<DeviceGroup> CreateAsync(DeviceGroup entity);
}

public class MongoRepository : IRepository
{
    private readonly IMongoCollection<DeviceGroup> _collection;

    public MongoRepository(IMongoDbCollectionFactory mongoDbDbCollectionFactory,
        IDatabaseSettings settings)
    {
        _collection =mongoDbDbCollectionFactory.GetCollection(settings);
    }

    public async Task<IEnumerable<DeviceGroup>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<DeviceGroup> GetByIdAsync(Guid id)
    {
        return await _collection.Find(entity => entity.Id == id).FirstOrDefaultAsync();
    }

    public async Task<DeviceGroup> CreateAsync(DeviceGroup entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }
}