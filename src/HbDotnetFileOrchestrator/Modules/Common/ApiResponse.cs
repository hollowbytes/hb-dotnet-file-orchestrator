using System;
using System.Diagnostics.CodeAnalysis;

namespace HbDotnetFileOrchestrator.Modules.Common;

[ExcludeFromCodeCoverage]
public record ApiResponse(
    Guid ConversationId
)
{
    public DateTime Timestamp => DateTime.UtcNow;
}

[ExcludeFromCodeCoverage]
public record ApiResponse<T>(
    Guid ConversationId,
    T Value
) : ApiResponse(ConversationId);