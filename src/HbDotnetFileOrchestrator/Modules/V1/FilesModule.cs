using CSharpFunctionalExtensions.HttpResults.ResultExtensions;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;
using HbDotnetFileOrchestrator.Modules.V1.Requests;
using Microsoft.AspNetCore.Mvc;

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

    private static async Task<IResult> PostFileAsync
    (
        [FromServices] LinkGenerator linker,
        [FromServices] ILogger<V1PostFileRequest> logger,
        [FromServices] IFilesService filesService,
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

        await using var contents = new MemoryStream();
        await request.File.CopyToAsync(contents);
        contents.Seek(0, SeekOrigin.Begin);
        var file = new ReceivedFile(request.File.FileName, request.File.Length, contents.ToArray());

        var result = await filesService.SaveFileAsync(file);
        return result.ToStatusCodeHttpResult(StatusCodes.Status202Accepted);
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