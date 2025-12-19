using System;

namespace HbDotnetFileOrchestrator.Modules.Common;

public record ApiResponse(
    Guid TraceId
)
{
    public DateTime Timestamp => DateTime.UtcNow;
}

public record ApiResponse<T>(
    Guid TraceId,
    T Data
) : ApiResponse(TraceId);