using Fluid;
using Fluid.Values;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage;

public class StorageLocationResolver : IFileLocationResolver
{
    private static readonly FluidParser PARSER = new();

    public ValueTask<string> ResolveAsync(Metadata metadata, IStorageOptions options,
        CancellationToken cancellationToken = default)
    {
        if (!PARSER.TryParse(options.Destination, out var template, out var error)) return ValueTask.FromResult(error);

        var parserOptions = new TemplateOptions
        {
            ModelNamesComparer = StringComparer.InvariantCultureIgnoreCase,
            MemberAccessStrategy = new UnsafeMemberAccessStrategy(),
            Undefined = name => ValueTask.FromResult<FluidValue>(new StringValue($"Error - '{name}' not found"))
        };

        var context = new TemplateContext(parserOptions);
        context.SetValue("metadata", metadata);
        return template.RenderAsync(context);
    }
}