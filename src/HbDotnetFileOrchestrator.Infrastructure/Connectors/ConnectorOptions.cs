using HbDotnetFileOrchestrator.Application.Files.Interfaces;
using HbDotnetFileOrchestrator.Infrastructure.Connectors.BlobStorage;
using HbDotnetFileOrchestrator.Infrastructure.Connectors.FileSystem;
using HbDotnetFileOrchestrator.Infrastructure.Connectors.Sftp;

namespace HbDotnetFileOrchestrator.Infrastructure.Connectors;

public class ConnectorOptions
{
    public const string SECTION = "Connectors";

    public BlobStorageConnectorOptions[] BlobStorage { get; set; } = [];

    public SftpConnectorOptions[] Sftp { get; set; } = [];

    public FileSystemConnectorOptions[] FileSystem { get; set; } = [];

    public IConnectorOptions[] All => typeof(ConnectorOptions).GetProperties()
        .Where(prop => prop.PropertyType.IsArray)
        .Where(prop => !prop.PropertyType.GetElementType()!.IsInterface)
        .Where(prop => typeof(IConnectorOptions[]).IsAssignableFrom(prop.PropertyType))
        .SelectMany(prop => prop.GetValue(this) as IConnectorOptions[] ?? [])
        .ToArray();

    public IConnectorOptions Find(string id)
    {
        return All.Single(x => x.Id == id);
    }
}