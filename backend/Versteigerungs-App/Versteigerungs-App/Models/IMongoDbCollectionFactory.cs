using MongoDB.Driver;

namespace Versteigerungs_App.Models;


public interface IMongoDbCollectionFactory<T>
{
    IMongoCollection<T> GetCollection(IDatabaseSettings settings);
}

public class MongoDbCollectionFactory<T> : IMongoDbCollectionFactory<T>
{
    public IMongoCollection<T> GetCollection(IDatabaseSettings settings) =>
        new MongoClient(settings.ConnectionString)
            .GetDatabase(settings.DatabaseName)
            .GetCollection<T>(settings.CollectionName);
}