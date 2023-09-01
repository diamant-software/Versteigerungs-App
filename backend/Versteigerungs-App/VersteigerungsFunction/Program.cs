using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Versteigerungs_App.Models;
using Versteigerungs_App.Services;

IConfigurationSection? sectionDevices = null;
IConfigurationSection? sectionBids = null;
IConfigurationSection? sectionAuction = null;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(configBuilder =>
    {
        var configurationRoot = configBuilder.Build();
        sectionDevices = configurationRoot.GetSection("MongoDBSettingsDevices");
        sectionBids = configurationRoot.GetSection("MongoDBSettingsBids");
        sectionAuction = configurationRoot.GetSection("MongoDBSettingsAuction");
    })
    .ConfigureServices(serviceCollection =>
    {
        serviceCollection.Configure<DatabaseSettings>("devices", sectionDevices);
        serviceCollection.Configure<DatabaseSettings>("bids", sectionBids);
        serviceCollection.Configure<DatabaseSettings>("auction", sectionAuction);
        
        serviceCollection.AddSingleton<IDatabaseSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<DatabaseSettings>>().Value);
        serviceCollection.AddSingleton<IMongoDbCollectionFactory<PersistedBid>, MongoDbCollectionFactory<PersistedBid>>();
        serviceCollection.AddSingleton<IMongoDbCollectionFactory<DeviceGroup>, MongoDbCollectionFactory<DeviceGroup>>();
        serviceCollection.AddSingleton<IMongoDbCollectionFactory<Auction>, MongoDbCollectionFactory<Auction>>();
        serviceCollection.AddSingleton<IDevicesRepository, MongoDevicesRepository>();
        serviceCollection.AddSingleton<IBiddingRepository, BiddingRepository>();
        serviceCollection.AddSingleton<IAuctionRepository, AuctionRepository>();
        serviceCollection.AddSingleton<IBiddingService, BiddingService>();
        serviceCollection.AddSingleton<IAuctionService, AuctionService>();
        
        serviceCollection
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

    })
    .Build();

host.Run();
