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

        if (httpContext is null)
        {
            // This is being called outside an http request lifecycle scope, or context accessor isn't registered
            throw new InvalidOperationException();
        }
        
        var routeValues = httpContext.Request.RouteValues.ToDictionary(kv => kv.Key, kv => kv.Value?.ToString());
        var headers = httpContext.Request.Headers.ToDictionary(x => x.Key, x => x.Value.ToArray());
        var query = httpContext.Request.Query.ToDictionary(kv => kv.Key, kv => kv.Value.ToArray());
        
        var files = await GetFormFiles(httpContext, cancellationToken);
        var form = await GetFormProperties(httpContext, cancellationToken);

        return new Metadata(headers, routeValues, query, form, files);
    }
    
    private static async Task<FileMetadata[]> GetFormFiles(HttpContext httpContext, CancellationToken cancellationToken = default)
    {
        if (!httpContext.Request.HasFormContentType) return [];
        
        var formCollection = await httpContext.Request.ReadFormAsync(cancellationToken);
        return formCollection.Files.Select(kv => new FileMetadata(
            kv.ContentType,
            kv.ContentDisposition,
            kv.Length,
            kv.Name,
            kv.FileName
        )).ToArray();
    }
    
    private static async Task<Dictionary<string, string?[]>> GetFormProperties(HttpContext httpContext, CancellationToken cancellationToken = default)
    {
        if (!httpContext.Request.HasFormContentType) return new Dictionary<string, string?[]>();
        
        var formCollection = await httpContext.Request.ReadFormAsync(cancellationToken);
        return formCollection.ToDictionary(kv => kv.Key, kv => kv.Value.ToArray());
    }
}