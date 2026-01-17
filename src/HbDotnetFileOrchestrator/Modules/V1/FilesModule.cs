using System.Net.Mime;
using CSharpFunctionalExtensions;
using CSharpFunctionalExtensions.HttpResults.ResultExtensions;
using FluentValidation;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Modules.Extensions;
using HbDotnetFileOrchestrator.Modules.V1.Requests;
using Microsoft.AspNetCore.Mvc;
using IResult = Microsoft.AspNetCore.Http.IResult;

namespace HbDotnetFileOrchestrator.Modules.V1;

public static class FilesModule
{
    public static void MapV1FilesModule(this IEndpointRouteBuilder endpoints)
    {
        var grouping = endpoints.MapGroup("files");

        grouping.MapPost("/", PostFileAsync)
            .WithName(nameof(PostFileAsync))
            .Accepts<V1PostFileRequest>(MediaTypeNames.Multipart.FormData)
            .DisableAntiforgery();

        grouping.MapGet("/{conversationId:guid}", GetFileAsync)
            .WithName(nameof(GetFileAsync));
    }

    private static async Task<IResult> PostFileAsync
    (
        [AsParameters] V1PostFileRequest request,
        [FromServices] ILogger<V1PostFileRequest> logger,
        [FromServices] IValidator<V1PostFileRequest> validator, 
        [FromServices] IFilesService filesService
    )
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            { "ConversationId", request.ConversationId },
            { "FileName", request.FormFile.FileName },
            { "FileLength", request.FormFile.Length }
        });
        
        logger.LogInformation("Received file");

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(
                errors: validationResult.ToDictionary(), 
                extensions: request.ToProblemDetailsExtensions()
            );
        }
        
        var file = await request.CopyFileAsync();
        var result = await filesService.SaveFileAsync(file);

        return result
            .Map(request.ToApiResponse)
            .ToCreatedHttpResult(customizeProblemDetails: details =>
            {
                details.Extensions = request.ToProblemDetailsExtensions();
            });
    }

    private static IResult GetFileAsync
    (
        [AsParameters] V1GetFileRequest request,
        [FromServices] ILogger<V1GetFileRequest> logger,
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