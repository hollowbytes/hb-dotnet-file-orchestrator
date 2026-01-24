using System.Net.Mime;
using FluentValidation;
using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Application.Files.Models;
using HbDotnetFileOrchestrator.Domain.Interfaces.Services;
using HbDotnetFileOrchestrator.Modules.Common;
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
        [FromServices] IFileWriterService fileWriterService
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
        var result = await fileWriterService.SaveFileAsync(file);
        
        var response = new ApiResponse<SavedFileResult[]>(request.ConversationId, result);
        return Results.Json(response, statusCode: StatusCodes.Status201Created);
    }

    private static async Task<IResult> GetFileAsync
    (
        [AsParameters] V1GetFileRequest request,
        [FromServices] ILogger<V1GetFileRequest> logger,
        [FromServices] IFileReaderService fileReaderService,
        CancellationToken cancellationToken)
    {
        using var scope = logger.BeginScope(new Dictionary<string, object>
        {
            { "ConversationId", request.ConversationId }
        });

        logger.LogInformation("Requesting file");

        await fileReaderService.ReadFileAsync(request.ConversationId, cancellationToken);
       
        return Results.File(new MemoryStream(), "application/text", "test.txt");
    }
}