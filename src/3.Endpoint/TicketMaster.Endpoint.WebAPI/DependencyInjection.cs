using TicketMaster.Infrastructure.Services;
using TicketMaster.Shared.Utilities.AppSettings;

namespace TicketMaster.Endpoint.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        services.ConfigureInfrastructure(configuration);
        //services.ConfigureApplicationService(configuration);

        return services;
    }
}