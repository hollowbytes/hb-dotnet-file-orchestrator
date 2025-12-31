using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HbDotnetFileOrchestrator.Infrastructure.Connectors;

public class ConnectorProvider(
    ILogger<ConnectorProvider> logger,
    IOptions<ConnectorOptions> connectorOptions,
    IServiceProvider serviceProvider
) : IConnectorProvider
{
    public IConnectorStrategy CreateConnector(IConnectorOptions options)
    {
        var connector = typeof(IConnectorStrategy<>).MakeGenericType(options.GetType());
        return (IConnectorStrategy)serviceProvider.GetRequiredService(connector);
    }
}