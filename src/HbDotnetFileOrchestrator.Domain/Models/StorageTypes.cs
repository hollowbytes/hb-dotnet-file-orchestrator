using System.Diagnostics.CodeAnalysis;

namespace HbDotnetFileOrchestrator.Domain.Models;

[ExcludeFromCodeCoverage]
public static class StorageTypes
{
    public static string BLOB = "blob";
    public static string SFTP = "sftp";
    public static string FILESYSTEM = "filesystem";
}