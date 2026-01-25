using System.IO.Abstractions;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Application.Files.Models.Commands;
using HbDotnetFileOrchestrator.Domain.Models;
using HbDotnetFileOrchestrator.Infrastructure.Sql.Models;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage.FileSystem;

public class FileSystemFileWriter
(
    ILogger<FileSystemFileWriter> logger,
    IFileSystem fileSystem
) : IFileWriterStrategy<FileSystemStorageDbo>
{
    public async Task<Result> SaveAsync(FileWriterCommand command, CancellationToken cancellationToken = default)
    {
        if (!fileSystem.Directory.Exists(command.Directory))
        {
            logger.LogInformation("Directory at '{Location}', attempting to create", command.Directory);
            fileSystem.Directory.CreateDirectory(command.Directory);
        }

        if (fileSystem.File.Exists(command.FullPath))
        {
            logger.LogInformation("File already exists at '{Path}'", command.FullPath);
            return Result.Failure($"File already exists at '{command.FullPath}'");
        }

        await fileSystem.File.WriteAllBytesAsync(command.FullPath, command.File.Contents, cancellationToken);
        logger.LogInformation("File written to '{Path}'", command.FullPath);
        return Result.Success();
    }
}