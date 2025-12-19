namespace HbDotnetFileOrchestrator.Infrastructure.Connectors.BlobStorage;

public class BlobStorageConnectorOptions : IConnectorOptions
{
    public string Id { get; set; } = string.Empty;

    public string Type => "BlobStorage";
}