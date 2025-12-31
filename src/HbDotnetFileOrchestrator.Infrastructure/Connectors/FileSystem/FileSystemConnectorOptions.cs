namespace HbDotnetFileOrchestrator.Infrastructure.Connectors.FileSystem;

public class FileSystemConnectorOptions : IConnectorOptions
{
    public string Id { get; set; }

    public string Type => "FileSystem";
}