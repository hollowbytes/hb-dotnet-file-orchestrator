using System.Collections.Generic;
using System.IO;
using System.Threading;
using HbDotnetFileOrchestrator.Modules.Common;
using HbDotnetFileOrchestrator.Modules.V1.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace HbDotnetFileOrchestrator.Modules.V1;

public static class FilesModule
{
    public static void MapV1FilesModule(this IEndpointRouteBuilder endpoints)
    {
        var grouping = endpoints.MapGroup("files");

        grouping.MapPost("/", PostFileAsync)
            .WithName(nameof(PostFileAsync))
            .DisableAntiforgery();

        grouping.MapGet("/{conversationId:guid}", GetFileAsync)
            .WithName(nameof(GetFileAsync));
    }

    private static IResult PostFileAsync
    (
        [FromServices] LinkGenerator linker,
        [FromServices] ILogger<V1PostFileRequest> logger,
        [AsParameters] V1PostFileRequest request
    )
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            { "ConversationId", request.ConversationId },
            { "FileName", request.File.FileName },
            { "FileLength", request.File.Length }
        });

        logger.LogInformation("Received file");

        // TODO: Store file

        var relativeUrl = linker.GetPathByName(nameof(GetFileAsync), new { request.ConversationId });
        return Results.Accepted(relativeUrl,
            new ApiResponse(request.ConversationId)
        );
    }

    private static IResult GetFileAsync
    (
        [FromServices] ILogger<V1GetFileRequest> logger,
        [AsParameters] V1GetFileRequest request,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            { "ConversationId", request.ConversationId }
        });

        logger.LogInformation("Requesting file");

        // TODO: Retrieve file

        return Results.File(new MemoryStream(), "application/text", "test.txt");
    }
}