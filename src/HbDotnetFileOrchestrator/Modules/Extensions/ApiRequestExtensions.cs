using HbDotnetFileOrchestrator.Modules.Common;

namespace HbDotnetFileOrchestrator.Modules.Extensions;

public static class ApiRequestExtensions
{
    extension<TRequest>(TRequest request) where TRequest : ApiRequest
    {
        public IDictionary<string, object?> ToProblemDetailsExtensions() => new Dictionary<string, object?>
        {
            { "conversationId", request.ConversationId }
        };

        public ApiResponse ToApiResponse() => new(request.ConversationId);
    }
}