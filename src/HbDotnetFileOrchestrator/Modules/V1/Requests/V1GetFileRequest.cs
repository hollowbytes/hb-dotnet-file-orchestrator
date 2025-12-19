using System;
using Microsoft.AspNetCore.Mvc;

namespace HbDotnetFileOrchestrator.Modules.V1.Requests;

public record V1GetFileRequest(
    [FromRoute] Guid ConversationId
);