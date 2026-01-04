using HbDotnetFileOrchestrator.Application.Files.Interfaces;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage.FileSystem;

public class FileSystemFileWriter : IFileWriter<FileSystemStorageOptions>
{
    public Task SaveAsync(string location, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}