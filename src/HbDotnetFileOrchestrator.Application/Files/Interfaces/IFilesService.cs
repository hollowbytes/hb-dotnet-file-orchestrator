namespace HbDotnetFileOrchestrator.Application.Files.Interfaces;

public interface IFilesService
{
    public Task SaveFileAsync(CancellationToken cancellationToken = default);
}