using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage;

public class FileWriterFactory(
    ILogger<FileWriterFactory> logger,
    IServiceProvider serviceProvider
) : IFileWriterFactory
{
    public IFileWriterStrategy Create(IFileDirectory fileDirectory)
    {
        var connector = typeof(IFileWriterStrategy<>).MakeGenericType(fileDirectory.GetType());
        return (IFileWriterStrategy)serviceProvider.GetRequiredService(connector);
    }
}