using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage;

public class StorageFactory(
    ILogger<StorageFactory> logger,
    IOptions<StorageOptions> connectorOptions,
    IServiceProvider serviceProvider
) : IFileWriterFactory
{
    public IFileWriter Create(IStorageOptions options)
    {
        var connector = typeof(IFileWriter<>).MakeGenericType(options.GetType());
        return (IFileWriter)serviceProvider.GetRequiredService(connector);
    }
}