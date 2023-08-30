using Microsoft.Extensions.Options;
using Versteigerungs_App.Models;
using Versteigerungs_App.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("MongoDBSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoDbCollectionFactory, MongoDbCollectionFactory>();


builder.Services.AddSingleton<IRepository, MongoRepository>();
builder.Services.AddSingleton<IBiddingService, BiddingService>();
builder.Services.AddControllers();

builder.Services.AddCors(p => p.AddPolicy("corsapp", policyBuilder =>
{
    policyBuilder
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .WithOrigins("http://localhost:3000","https://*.diamant-software.de/rem*")
        .AllowAnyMethod()
        .AllowCredentials()
        .AllowAnyHeader();
}));

var app = builder.Build();


app.UseCors("corsapp");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();


app.Run();