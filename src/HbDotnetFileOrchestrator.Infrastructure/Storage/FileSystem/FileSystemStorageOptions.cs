using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage.FileSystem;

public class FileSystemStorageOptions : IStorageOptions
{
    public string Id { get; set; }

    public string Rule { get; set; } = string.Empty;
    
    public string Destination { get; set; } = string.Empty;

    public string Type => StorageTypes.FILESYSTEM;
}