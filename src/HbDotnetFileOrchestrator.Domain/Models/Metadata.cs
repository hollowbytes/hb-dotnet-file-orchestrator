namespace HbDotnetFileOrchestrator.Domain.Models;

public record Metadata(
    Dictionary<string, string?[]> Headers,
    Dictionary<string, string?> RouteValues,
    Dictionary<string, string?[]> Query,
    Dictionary<string, string?[]> Form,
    FileMetadata[] Files
)
{
    public static Metadata EMPTY => new(new Dictionary<string, string?[]>(), new Dictionary<string, string?>(), new Dictionary<string, string?[]>(),new Dictionary<string, string?[]>(), []);
}