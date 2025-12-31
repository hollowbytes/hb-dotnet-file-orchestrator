namespace HbDotnetFileOrchestrator.Domain.Models;

public record Metadata(
    IDictionary<string, string?[]> Headers,
    IDictionary<string, string?[]> Query);