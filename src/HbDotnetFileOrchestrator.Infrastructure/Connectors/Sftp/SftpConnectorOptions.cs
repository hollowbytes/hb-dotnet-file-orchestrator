using HbDotnetFileOrchestrator.Application.Files.Interfaces;

namespace HbDotnetFileOrchestrator.Infrastructure.Connectors.Sftp;

public class SftpConnectorOptions : IConnectorOptions
{
    public string Id { get; set; } = string.Empty;

    public string Rule { get; set; } = string.Empty;

    public string Type => ConnectionTypes.SFTP;
}