using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents()
        {
            OnAuthenticationFailed = c =>
            {
                Console.WriteLine();
                return null!;
            }

        };

        var url = "https://sts.windows.net/393f7f62-ffae-4740-b443-bd04273d7320/";
        options.RequireHttpsMetadata = false;
        options.Authority = url;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidIssuer = url,
            ValidTypes = new[] { "JWT" }
        };

        options.SecurityTokenValidators.Clear();
        options.SecurityTokenValidators.Add(new JwtSecurityTokenHandler
        {
            MapInboundClaims = false
        });
    });

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