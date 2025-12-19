using HbDotnetFileOrchestrator.Infrastructure.Authentication;
using HbDotnetFileOrchestrator.Infrastructure.Connectors;
using Microsoft.Extensions.DependencyInjection;

namespace HbDotnetFileOrchestrator.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddOptions<AuthenticationOptions>()
            .BindConfiguration(AuthenticationOptions.SECTION)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<ConnectorOptions>()
            .BindConfiguration(ConnectorOptions.SECTION)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}