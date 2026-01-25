using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage;

public class StorageReaderFactory(
    ILogger<StorageReaderFactory> logger,
    IServiceProvider serviceProvider
) : IFileReaderFactory
{
    public IFileReaderStrategy Create(IFileDestination fileDestination)
    {
        var connector = typeof(IFileReaderStrategy<>).MakeGenericType(fileDestination.GetType());
        return (IFileReaderStrategy)serviceProvider.GetRequiredService(connector);
    }
}