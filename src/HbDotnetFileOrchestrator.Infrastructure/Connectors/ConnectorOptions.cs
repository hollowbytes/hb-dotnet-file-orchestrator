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

    public IConnectorOptions[] All
    {
        get
        {
            var props = typeof(ConnectorOptions).GetProperties();
            var filteredProps = props
                .Where(prop => prop.PropertyType.IsArray)
                .Where(prop => !prop.PropertyType.GetElementType()!.IsInterface)
                .Where(prop => typeof(IConnectorOptions[]).IsAssignableFrom(prop.PropertyType));

            var values = filteredProps.SelectMany(prop =>
                    prop.GetValue(this) as IConnectorOptions[] ?? Array.Empty<IConnectorOptions>())
                .ToArray();

            return values;
        }
    }

    public IConnectorOptions Find(string id)
    {
        return All.Single(x => x.Id == id);
    }
}