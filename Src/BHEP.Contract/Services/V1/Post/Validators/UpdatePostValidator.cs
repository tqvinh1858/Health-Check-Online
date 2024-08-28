using FluentValidation;

namespace BHEP.Contract.Services.V1.Post.Validators;
public class UpdatePostValidator : AbstractValidator<Command.UpdatePostCommand>
{
    public UpdatePostValidator()
    {
        RuleFor(x => x.Gender).IsInEnum().WithMessage("Invalid gender value.");
        RuleFor(x => x.Status).IsInEnum().WithMessage("Invalid status value.");
    }
}
