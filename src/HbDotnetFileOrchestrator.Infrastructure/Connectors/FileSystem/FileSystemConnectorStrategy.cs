using HbDotnetFileOrchestrator.Application.Files.Interfaces;

namespace HbDotnetFileOrchestrator.Infrastructure.Connectors.FileSystem;

public class FileSystemConnectorStrategy : IConnectorStrategy<FileSystemConnectorOptions>
{
    public Task SaveAsync(FileSystemConnectorOptions options, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}