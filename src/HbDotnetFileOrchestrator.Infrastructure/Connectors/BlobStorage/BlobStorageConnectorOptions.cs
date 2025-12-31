using HbDotnetFileOrchestrator.Application.Files.Interfaces;

namespace HbDotnetFileOrchestrator.Infrastructure.Connectors.BlobStorage;

public class BlobStorageConnectorOptions : IConnectorOptions
{
    public string Id { get; set; } = string.Empty;

    public string Rule { get; set; } = string.Empty;

    public string Type => ConnectionTypes.BLOB;
}