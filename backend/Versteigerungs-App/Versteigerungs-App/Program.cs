using MongoDB.Driver;
using Versteigerungs_App.Services;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var mongoSettings = builder.Configuration.GetSection("MongoDBSettings");
builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(mongoSettings["ConnectionString"]));
builder.Services.AddScoped<IMongoDatabase>(s =>
{
    var client = s.GetRequiredService<IMongoClient>();
    return client.GetDatabase(mongoSettings["DatabaseName"]);
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

app.Run();