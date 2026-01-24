using System.Diagnostics.CodeAnalysis;

namespace HbDotnetFileOrchestrator.Modules.Common;

[ExcludeFromCodeCoverage]
public abstract record ApiRequest
{
    public virtual Guid ConversationId { get; } = Guid.NewGuid();
}