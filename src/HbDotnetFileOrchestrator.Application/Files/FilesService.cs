using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Application.Files;

public class FilesService(
    ILogger<FilesService> logger,
    IMetadataProvider metadataProvider,
    IConnectorProvider connectorProvider
) : IFilesService
{
    public async Task SaveFileAsync(CancellationToken cancellationToken = default)
    {
        var metadata = metadataProvider.GetMetadata();
        var providers = await connectorProvider.GetProvidersAsync(metadata, cancellationToken);
    }
}