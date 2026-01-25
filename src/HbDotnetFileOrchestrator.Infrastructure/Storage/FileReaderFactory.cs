using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage;

public class FileReaderFactory(
    ILogger<FileReaderFactory> logger,
    IServiceProvider serviceProvider
) : IFileReaderFactory
{
    public IFileReaderStrategy Create(IFileDirectory fileDirectory)
    {
        var connector = typeof(IFileReaderStrategy<>).MakeGenericType(fileDirectory.GetType());
        return (IFileReaderStrategy)serviceProvider.GetRequiredService(connector);
    }
}