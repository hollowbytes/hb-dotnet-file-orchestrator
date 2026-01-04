using HbDotnetFileOrchestrator.Domain.Interfaces;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileWriter
{
    Task SaveAsync(string location, CancellationToken cancellationToken = default);
}

public interface IFileWriter<in TOptions> : IFileWriter where TOptions : class, IStorageOptions;