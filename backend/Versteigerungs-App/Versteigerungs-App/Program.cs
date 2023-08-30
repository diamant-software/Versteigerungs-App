using Microsoft.Extensions.Options;
using Versteigerungs_App.Models;
using Versteigerungs_App.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoDbCollectionFactory, MongoDbCollectionFactory>();


builder.Services.AddSingleton<IRepository, MongoRepository>();
builder.Services.AddControllers();
var app = builder.Build();
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();


app.Run();