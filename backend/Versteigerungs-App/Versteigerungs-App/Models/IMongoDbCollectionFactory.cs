using MongoDB.Bson;
using MongoDB.Driver;

namespace Versteigerungs_App.Models;


public interface IMongoDbCollectionFactory
{
    IMongoCollection<DeviceGroup> GetCollection(IDatabaseSettings settings);
}

public class MongoDbCollectionFactory : IMongoDbCollectionFactory
{
    public IMongoCollection<DeviceGroup> GetCollection(IDatabaseSettings settings) =>
        new MongoClient(settings.ConnectionString)
            .GetDatabase(settings.DatabaseName)
            .GetCollection<DeviceGroup>(settings.CollectionName);
}