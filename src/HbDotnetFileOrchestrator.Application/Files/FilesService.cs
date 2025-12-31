using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Application.Files;

public class FilesService(
    ILogger<FilesService> logger,
    IMetadataProvider metadataProvider,
    IRulesEngine rulesEngine,
    IConnectorProvider connectorProvider
) : IFilesService
{
    public async Task SaveFileAsync(CancellationToken cancellationToken = default)
    {
        var metadata = metadataProvider.GetMetadata();
        var providers = await rulesEngine.RunAsync(metadata, cancellationToken);

        foreach (var options in providers)
        {
            var provider = connectorProvider.CreateConnector(options);

            await provider.SaveAsync(options, cancellationToken);
        }
    }
}