using TicketMaster.Core.ApplicationService;
using TicketMaster.Endpoint.WebAPI.Endpoints;
using TicketMaster.Infrastructure.Services;
using TicketMaster.Shared.Utilities.AppSettings;

namespace TicketMaster.Endpoint.WebAPI;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));

        services.ConfigureInfrastructure(configuration);
        services.ConfigureApplicationService(configuration);

        return services;
    }

    public static void MapEndpoints(this WebApplication app)
    {
        var assembly = typeof(IEndpoint).Assembly;

        var endpoints = assembly.DefinedTypes
            .Where(t => !t.IsAbstract && !t.IsInterface && t.IsAssignableTo(typeof(IEndpoint)))
            .Select(t => (IEndpoint)Activator.CreateInstance(t.AsType())!).ToArray();

        foreach (var endpoint in endpoints)
            endpoint.Register(app);
    }
}