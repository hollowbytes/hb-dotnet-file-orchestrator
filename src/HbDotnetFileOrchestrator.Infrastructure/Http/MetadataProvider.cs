using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Infrastructure.Http;

public class MetadataProvider(ILogger<MetadataProvider> logger, IHttpContextAccessor httpContextAccessor)
    : IMetadataProvider
{
    public async Task<Metadata> GetMetadataAsync(CancellationToken cancellationToken = default)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            return new Metadata(
                new Dictionary<string, string?[]>(),
                new Dictionary<string, string?[]>(),
                new Dictionary<string, string?[]>(),
                []
            );
        }

        var headers = httpContext.Request.Headers.ToDictionary(x => x.Key, x => x.Value.ToArray());
        var query = httpContext.Request.Query.ToDictionary(kv => kv.Key, kv => kv.Value.ToArray());

        var formCollection = await httpContext.Request.ReadFormAsync(cancellationToken);
        var form = formCollection.ToDictionary(kv => kv.Key, kv => kv.Value.ToArray());

        var files = formCollection.Files.Select(kv => new FileMetadata(
            kv.ContentType,
            kv.ContentDisposition,
            kv.Length,
            kv.Name,
            kv.FileName
        ))
        .ToArray();

        return new Metadata(headers, query, form, files);
    }
}