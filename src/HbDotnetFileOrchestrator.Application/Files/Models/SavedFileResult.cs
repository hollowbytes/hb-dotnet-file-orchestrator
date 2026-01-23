namespace HbDotnetFileOrchestrator.Application.Files.Models;

public record SavedFileResult
(
  string RuleName,
  string StorageName,
  string StorageType,
  string FileName
)
{
  public string? FileLocation { get; init; }
  
  public string? Error { get; init; }
}