namespace HbDotnetFileOrchestrator.Domain.Models;

public record ReceivedFile(
    string Name,
    long Size,
    byte[] Contents
)
{
    public DateTime ReceivedDate => DateTime.UtcNow;
};