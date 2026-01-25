using HbDotnetFileOrchestrator.Domain.Interfaces;

namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFileReaderStrategy
{
    Task<string> ReadFileAsync(string location, CancellationToken cancellationToken = default);
}

public interface IFileReaderStrategy<in TOptions> : IFileReaderStrategy where TOptions : class, IFileDirectory;