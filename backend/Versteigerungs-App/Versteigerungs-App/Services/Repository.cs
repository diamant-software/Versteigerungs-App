namespace Versteigerungs_App.Services;

using MongoDB.Driver;


// IRepository.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T entity);
    // Weitere Methoden je nach Bedarf
}



public class MongoRepository<T> : IRepository<T>
{
    private readonly IMongoCollection<T> _collection;

    public MongoRepository(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<T>(collectionName);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _collection.Find(entity => GetId(entity) == id).FirstOrDefaultAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }
    

    private Guid GetId(T entity)
    {
        var property = entity.GetType().GetProperty("Id");
        return (Guid)property.GetValue(entity);
    }
}