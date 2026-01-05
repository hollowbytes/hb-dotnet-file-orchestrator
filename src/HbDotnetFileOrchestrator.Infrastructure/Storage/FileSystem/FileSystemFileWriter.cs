using CSharpFunctionalExtensions;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage.FileSystem;

public class FileSystemFileWriter : IFileWriter<FileSystemStorageOptions>
{
    public async Task<Result> SaveAsync(ReceivedFile file, string location, CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(location))
        {
            Directory.CreateDirectory(location);
        }

        var path = Path.Combine(location, file.Name);

        if (File.Exists(path))
        {
            return Result.Failure($"File '{path}' already exists");
        }

        await File.WriteAllBytesAsync(path, file.Contents, cancellationToken);
        return Result.Success();
    }
}