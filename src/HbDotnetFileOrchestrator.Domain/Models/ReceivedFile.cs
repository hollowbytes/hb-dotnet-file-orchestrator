using System.Diagnostics.CodeAnalysis;

namespace HbDotnetFileOrchestrator.Domain.Models;

[ExcludeFromCodeCoverage]
public record ReceivedFile(
    string Name,
    long Size,
    byte[] Contents
)
{
    public DateTime ReceivedDate => DateTime.UtcNow;
};