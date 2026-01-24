using System.Diagnostics.CodeAnalysis;

namespace HbDotnetFileOrchestrator.Domain.Models;

[ExcludeFromCodeCoverage]
public record ReceivedFile(
    Guid ConversationId,
    string Name,
    long Size,
    byte[] Contents
)
{
    public DateTime ReceivedDate { get; } = DateTime.UtcNow;
};