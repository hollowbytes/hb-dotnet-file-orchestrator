using System.Diagnostics.CodeAnalysis;
using HbDotnetFileOrchestrator.Domain.Interfaces;
using HbDotnetFileOrchestrator.Domain.Models;

namespace HbDotnetFileOrchestrator.Infrastructure.Storage.Blob;

[ExcludeFromCodeCoverage]
public class BlobStorageOptions : IStorageOptions
{
    public string Id { get; set; } = string.Empty;

    public string Rule { get; set; } = string.Empty;

    public string Destination { get; set; } = string.Empty;

    public string Type => StorageTypes.BLOB;
}