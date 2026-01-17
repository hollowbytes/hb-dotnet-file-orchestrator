using HbDotnetFileOrchestrator.Domain.Models;
using HbDotnetFileOrchestrator.Modules.Common;
using Microsoft.AspNetCore.Mvc;

namespace HbDotnetFileOrchestrator.Modules.V1.Requests;

public record V1PostFileRequest([FromForm(Name = "file")] IFormFile FormFile) : ApiRequest
{
    public async Task<ReceivedFile> CopyFileAsync(CancellationToken cancellationToken = default)
    {
        await using var contents = new MemoryStream();
        await FormFile.CopyToAsync(contents, cancellationToken);
        contents.Seek(0, SeekOrigin.Begin);
        return new ReceivedFile(FormFile.FileName, FormFile.Length, contents.ToArray());
    }
}