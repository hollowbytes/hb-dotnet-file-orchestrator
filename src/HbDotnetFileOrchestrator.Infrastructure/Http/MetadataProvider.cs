using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Infrastructure.Http;

public class MetadataProvider(ILogger<MetadataProvider> logger, IHttpContextAccessor httpContextAccessor)
    : IMetadataProvider
{
    private HttpContext HttpContext => httpContextAccessor.HttpContext;

    public Metadata GetProperties()
    {
        var headers = HttpContext.Request.Headers.ToDictionary(x => x.Key, x => x.Value.ToArray());
        var query = HttpContext.Request.Query.ToDictionary(kv => kv.Key, kv => kv.Value.ToArray());

        return new Metadata(headers, query);
    }
}