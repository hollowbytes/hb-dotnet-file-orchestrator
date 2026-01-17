using FluentValidation;
using HbDotnetFileOrchestrator.Modules.V1.Requests;

namespace HbDotnetFileOrchestrator.Modules.V1.Validators;

public class V1PostFileRequestValidator : AbstractValidator<V1PostFileRequest>
{
    public V1PostFileRequestValidator()
    {
        RuleFor(rf => rf.FormFile).NotNull();
        RuleFor(rf => rf.FormFile.Length).GreaterThan(0);
    }
}