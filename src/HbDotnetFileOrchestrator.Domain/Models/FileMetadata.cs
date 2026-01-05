namespace HbDotnetFileOrchestrator.Domain.Models;

public record FileMetadata(
    string ContentType,
    /// <summary>
    /// Gets the raw Content-Disposition header of the uploaded file.
    /// </summary>
    string ContentDisposition,
    /// <summary>
    /// Gets the file length in bytes.
    /// </summary>
    long Length,
    /// <summary>
    /// Gets the form field name from the Content-Disposition header.
    /// </summary>
    string Name,
    /// <summary>
    /// Gets the file name from the Content-Disposition header.
    /// </summary>
    string FileName
);