using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;
using TicketMaster.Infrastructure.Data;

namespace TicketMaster.Infrastructure.Services;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TicketMasterDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("TicketMaster"));
        });

        services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var options = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis")!);
            options.AbortOnConnectFail = false;

            return ConnectionMultiplexer.Connect(options);
        });

        services.AddSingleton<IDistributedLockFactory>(sp =>
        {
            var multiplexers = new List<RedLockMultiplexer>()
            {
                (RedLockMultiplexer)sp.GetRequiredService<IConnectionMultiplexer>()
            };
            return RedLockFactory.Create(multiplexers);
        });

        services.AddMemoryCache();

        return services;
    }
}