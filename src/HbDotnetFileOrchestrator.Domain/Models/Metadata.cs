using System.Diagnostics.CodeAnalysis;

namespace HbDotnetFileOrchestrator.Domain.Models;

[ExcludeFromCodeCoverage]
public record Metadata(
    Dictionary<string, string?[]> Headers,
    Dictionary<string, string?> RouteValues,
    Dictionary<string, string?[]> Query,
    Dictionary<string, string?[]> Form,
    FileMetadata[] Files
);