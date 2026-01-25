using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Application.Files.Models.Commands;

public record FileWriterCommand(
    ReceivedFile File,
    string Directory
)
{
    public string FullPath => Path.Combine(Directory, File.Name);
}