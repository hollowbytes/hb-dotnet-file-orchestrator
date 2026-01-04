using CSharpFunctionalExtensions;
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
    public async Task<Result> SaveFileAsync(CancellationToken cancellationToken = default)
    {
        var metadata = metadataProvider.GetMetadata();
        var providers = await ruleEvaluator.RunAsync(metadata, cancellationToken);

        foreach (var options in providers)
        {
            var locationResult = await fileLocationResolver.ResolveAsync(metadata, options, cancellationToken);

            if (locationResult.IsFailure) return locationResult;

            var location = locationResult.Value;
            var provider = fileWriterFactory.Create(options);
            await provider.SaveAsync(location, cancellationToken);
        }

        return Result.Success();
    }
}