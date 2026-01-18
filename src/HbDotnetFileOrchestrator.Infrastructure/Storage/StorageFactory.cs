using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage;

public class StorageFactory(
    ILogger<StorageFactory> logger,
    IServiceProvider serviceProvider
) : IFileWriterFactory
{
    public IFileWriter Create(IFileDestination fileDestination)
    {
        var connector = typeof(IFileWriter<>).MakeGenericType(fileDestination.GetType());
        return (IFileWriter)serviceProvider.GetRequiredService(connector);
    }
}