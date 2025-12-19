using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HbDotnetFileOrchestrator.Modules.V1.Requests;

public record V1PostFileRequest([FromForm] IFormFile File)
{
    public Guid ConversationId => Guid.NewGuid();
}