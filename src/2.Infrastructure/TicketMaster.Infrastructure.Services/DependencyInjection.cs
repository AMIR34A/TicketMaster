using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedLockNet;
using RedLockNet.SERedis;
using RedLockNet.SERedis.Configuration;
using StackExchange.Redis;
using TicketMaster.Core.Contracts.Data.Repositories;
using TicketMaster.Infrastructure.Data;
using TicketMaster.Infrastructure.Data.Repositories;

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

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();

        return services;
    }
}