namespace HbDotnetFileOrchestrator.Domain.Models;

public record Metadata(
    Dictionary<string, string?[]> Headers,
    Dictionary<string, string?[]> Query,
    Dictionary<string, string?[]> Form,
    FileMetadata[] Files
);