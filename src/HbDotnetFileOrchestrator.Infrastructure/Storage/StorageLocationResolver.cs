using System.Text.Json;
using CSharpFunctionalExtensions;
using Fluid;
using Fluid.Values;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage;

public class StorageLocationResolver : IFileLocationResolver
{
    private static readonly FluidParser PARSER = new();

    public async Task<Result<string>> ResolveAsync(Metadata metadata, IStorageOptions options,
        CancellationToken cancellationToken = default)
    {
        if (!PARSER.TryParse(options.Destination, out var template, out var error))
            return Result.Failure<string>(error);

        var parserOptions = new TemplateOptions
        {
            ModelNamesComparer = StringComparer.InvariantCultureIgnoreCase,
            MemberAccessStrategy = new UnsafeMemberAccessStrategy(),
            Undefined = name => ValueTask.FromResult<FluidValue>(new StringValue($"Error - '{name}' not found"))
        };

        var context = new TemplateContext(parserOptions);
        context.SetValue("metadata", metadata);
        
        var location = await template.RenderAsync(context);
        return Result.Success(location);
    }
}