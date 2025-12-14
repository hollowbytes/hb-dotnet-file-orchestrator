namespace HbDotnetFileOrchestrator.Modules.V1;

public static class FilesModule
{
    public static void MapV1FilesModule(this IEndpointRouteBuilder endpoints)
    {
        var grouping = endpoints.MapGroup("files");
        grouping.MapPost("/", PostFileAsync);
        grouping.MapGet("/", GetFileAsync);
    }

    private static IResult PostFileAsync()
    {
        return Results.Accepted();
    }

    private static IResult GetFileAsync()
    {
        return Results.Ok();
    }
}