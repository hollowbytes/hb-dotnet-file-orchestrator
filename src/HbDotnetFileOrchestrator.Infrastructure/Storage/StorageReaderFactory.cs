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
    public IFileReader Create(IFileDestination fileDestination)
    {
        var connector = typeof(IFileReader<>).MakeGenericType(fileDestination.GetType());
        return (IFileReader)serviceProvider.GetRequiredService(connector);
    }
}