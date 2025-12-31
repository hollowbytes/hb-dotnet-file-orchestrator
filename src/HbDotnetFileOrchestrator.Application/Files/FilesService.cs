using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Application.Files;

public class FilesService(
    ILogger<FilesService> logger,
    IMetadataProvider metadataProvider
) : IFilesService
{
    public Task SaveFileAsync(CancellationToken cancellationToken = default)
    {
        var metadata = metadataProvider.GetProperties();

        return Task.CompletedTask;
    }
}