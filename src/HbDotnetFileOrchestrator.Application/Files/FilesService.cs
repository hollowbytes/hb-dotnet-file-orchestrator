using CSharpFunctionalExtensions;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces.Repositories;
using HbDotnetFileOrchestrator.Domain.Interfaces.Services;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Application.Files;

public class FilesService(
    ILogger<FilesService> logger,
    IMetadataProvider metadataProvider,
    IRuleRepository ruleRepository,
    IRuleEvaluator ruleEvaluator,
    IIFileDestinationRepository fileDestinationRepository,
    IFileLocationResolver fileLocationResolver,
    IFileWriterFactory fileWriterFactory
) : IFilesService
{
    public async Task<Result> SaveFileAsync(ReceivedFile receivedFile, CancellationToken cancellationToken = default)
    {
        var metadata = await metadataProvider.GetMetadataAsync(cancellationToken);
        
        var rules = await ruleRepository.GetAllAsync(cancellationToken);
        var evaluatedRules = await ruleEvaluator.RunAsync(rules, metadata, cancellationToken);

        foreach (var rule in evaluatedRules)
        {
            var destinations = await fileDestinationRepository.GetDestinationsByRuleAsync(rule, cancellationToken);

            foreach (var destination in destinations)
            {
                var locationResult = await fileLocationResolver.ResolveAsync(metadata, destination, cancellationToken);

                if (locationResult.IsFailure)
                {
                    return locationResult;
                }

                var provider = fileWriterFactory.Create(destination);
                return await provider.SaveAsync(receivedFile, locationResult.Value, cancellationToken);
            }
        }
        return Result.Success();
    }
}