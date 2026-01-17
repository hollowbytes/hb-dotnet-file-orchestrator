using System;
using System.Diagnostics.CodeAnalysis;
using HbDotnetFileOrchestrator.Modules.Common;
using Microsoft.AspNetCore.Mvc;

namespace HbDotnetFileOrchestrator.Modules.V1.Requests;

[ExcludeFromCodeCoverage]
public record V1GetFileRequest(
    [FromRoute] Guid ConversationId
) : ApiRequest;