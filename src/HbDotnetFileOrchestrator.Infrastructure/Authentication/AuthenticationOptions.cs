using HbDotnetFileOrchestrator.Infrastructure.Authentication.BlobStorage;
using HbDotnetFileOrchestrator.Infrastructure.Authentication.Sftp;

namespace HbDotnetFileOrchestrator.Infrastructure.Authentication;

public class AuthenticationOptions
{
    public const string SECTION = "Authentication";

    public Dictionary<string, BlobStorageAuthenticationOptions> BlobStorage { get; set; } = new();

    public Dictionary<string, SftpAuthenticationOptions> Sftp { get; set; } = new();
}