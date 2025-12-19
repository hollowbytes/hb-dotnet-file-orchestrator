using HbDotnetFileOrchestrator.Infrastructure.Connectors.BlobStorage;
using HbDotnetFileOrchestrator.Infrastructure.Connectors.Sftp;

namespace HbDotnetFileOrchestrator.Infrastructure.Connectors;

public class ConnectorOptions
{
    public const string SECTION = "Connectors";

    public BlobStorageConnectorOptions[] BlobStorage { get; set; } = [];

    public SftpConnectorOptions[] Sftp { get; set; } = [];

    public IConnectorOptions[] All => Enumerable.Empty<IConnectorOptions>()
        .Concat(BlobStorage)
        .Concat(Sftp)
        .ToArray();

    public IConnectorOptions Find(string id)
    {
        return All.Single(x => x.Id == id);
    }
}