using System.IO.Abstractions;
using CSharpFunctionalExtensions;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage.FileSystem;

public class FileSystemFileWriter
(
    ILogger<FileSystemFileWriter> logger,
    IFileSystem fileSystem
) : IFileWriter<FileSystemStorageOptions>
{
    public async Task<Result> SaveAsync(ReceivedFile file, string location, CancellationToken cancellationToken = default)
    {
        if (!fileSystem.Directory.Exists(location))
        {
            logger.LogInformation("Directory at '{Location}', attempting to create", location);
            fileSystem.Directory.CreateDirectory(location);
        }

        var path = Path.Combine(location, file.Name);

        if (fileSystem.File.Exists(path))
        {
            logger.LogInformation("File already exists at '{Path}'", path);
            return Result.Failure($"File already exists at '{path}'");
        }

        await fileSystem.File.WriteAllBytesAsync(path, file.Contents, cancellationToken);
        logger.LogInformation("File written to '{Path}'", path);
        return Result.Success();
    }
}