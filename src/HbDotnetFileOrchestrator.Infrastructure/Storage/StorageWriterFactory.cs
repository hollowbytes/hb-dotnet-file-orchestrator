using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage;

public class StorageWriterFactory(
    ILogger<StorageWriterFactory> logger,
    IServiceProvider serviceProvider
) : IFileWriterFactory
{
    public IFileWriterStrategy Create(IFileDestination fileDestination)
    {
        var connector = typeof(IFileWriterStrategy<>).MakeGenericType(fileDestination.GetType());
        return (IFileWriterStrategy)serviceProvider.GetRequiredService(connector);
    }
}