using System.Diagnostics.CodeAnalysis;

namespace HbDotnetFileOrchestrator.Modules.Common;

[ExcludeFromCodeCoverage]
public record ApiRequest
{
    public Guid ConversationId { get;  } = Guid.NewGuid();
}