using CSharpFunctionalExtensions;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;
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
    public async Task<Result> SaveFileAsync(ReceivedFile receivedFile, CancellationToken cancellationToken = default)
    {
        var metadata = await metadataProvider.GetMetadataAsync(cancellationToken);
        var providers = await ruleEvaluator.RunAsync(metadata, cancellationToken);

        foreach (var options in providers)
        {
            var locationResult = await fileLocationResolver.ResolveAsync(metadata, options, cancellationToken);

            if (locationResult.IsFailure)
            {
                return locationResult;
            }

            var location = locationResult.Value;
            var provider = fileWriterFactory.Create(options);
            return await provider.SaveAsync(receivedFile, location, cancellationToken);
        }
        return Result.Success();
    }
}