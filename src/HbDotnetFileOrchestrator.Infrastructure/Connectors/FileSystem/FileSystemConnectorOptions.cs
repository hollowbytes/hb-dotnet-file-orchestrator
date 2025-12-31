namespace HbDotnetFileOrchestrator.Infrastructure.Connectors.FileSystem;

public class FileSystemConnectorOptions : IConnectorOptions
{
    public string Id { get; set; }

    public string Rule { get; set; } = string.Empty;

    public string Type => ConnectionTypes.FILESYSTEM;
}