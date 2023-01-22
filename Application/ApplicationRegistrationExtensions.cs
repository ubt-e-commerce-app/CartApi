using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Application;

public static class ApplicationRegistrationExtensions
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services, IConfiguration _)
    {
        // Use the invariant culture throughout the application
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

        // Register the current project's dependencies
        services.Scan(scanner => scanner.FromAssemblies(typeof(ApplicationRegistrationExtensions).Assembly)
            .AddClasses(c => c.Where(type => !type.IsNested), publicOnly: false)
            .AsSelfWithInterfaces().WithScopedLifetime());

        return services;
    }
}
