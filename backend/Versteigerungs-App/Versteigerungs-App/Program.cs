using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using Versteigerungs_App.Models;
using Versteigerungs_App.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>("devices",builder.Configuration.GetSection("MongoDBSettingsDevices"));
builder.Services.Configure<DatabaseSettings>("bids",builder.Configuration.GetSection("MongoDBSettingsBids"));
builder.Services.Configure<DatabaseSettings>("auction",builder.Configuration.GetSection("MongoDBSettingsAuction"));

builder.Services.AddSingleton<IDatabaseSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value);
builder.Services.AddSingleton<IMongoDbCollectionFactory<PersistedBid>, MongoDbCollectionFactory<PersistedBid>>();
builder.Services.AddSingleton<IMongoDbCollectionFactory<DeviceGroup>, MongoDbCollectionFactory<DeviceGroup>>();
builder.Services.AddSingleton<IMongoDbCollectionFactory<Auction>, MongoDbCollectionFactory<Auction>>();
builder.Services.AddSingleton<IDevicesRepository, MongoDevicesRepository>();
builder.Services.AddSingleton<IBiddingRepository, BiddingRepository>();
builder.Services.AddSingleton<IAuctionRepository, AuctionRepository>();
builder.Services.AddSingleton<IBiddingService, BiddingService>();
builder.Services.AddSingleton<IAuctionService, AuctionService>();
builder.Services.AddControllers();

builder.Services.AddCors(p => p.AddPolicy("corsapp", policyBuilder =>
{
    policyBuilder
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .WithOrigins("http://localhost:3000","https://*.diamant-software.de/*")
        .AllowAnyMethod()
        .AllowCredentials()
        .AllowAnyHeader();
}));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(options =>
    {
        builder.Configuration.Bind("AzureAdB2C", options);

        options.TokenValidationParameters.NameClaimType = "name";
    }, 
        options => { builder.Configuration.Bind("AzureAd", options); });


var app = builder.Build();

app.UseCookiePolicy(new CookiePolicyOptions
{
    Secure = CookieSecurePolicy.Always
});
app.UseCors("corsapp");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();


app.Run();