using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Application.Files;

public class FilesService(
    ILogger<FilesService> logger,
    IMetadataProvider metadataProvider,
    IRuleEvaluator ruleEvaluator,
    IFileLocationResolver fileLocationResolver,
    IFileWriterFactory fileWriterFactory
) : IFilesService
{
    public async Task SaveFileAsync(CancellationToken cancellationToken = default)
    {
        var metadata = metadataProvider.GetMetadata();
        var providers = await ruleEvaluator.RunAsync(metadata, cancellationToken);

        foreach (var options in providers)
        {
            var location = await fileLocationResolver.ResolveAsync(metadata, options, cancellationToken);

            var provider = fileWriterFactory.Create(options);
            await provider.SaveAsync(location, cancellationToken);
        }
    }
}