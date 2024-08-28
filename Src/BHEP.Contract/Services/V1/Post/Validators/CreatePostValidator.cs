using FluentValidation;

namespace BHEP.Contract.Services.V1.Post.Validators;
public class CreatePostValidator : AbstractValidator<Command.CreatePostCommand>
{
    public CreatePostValidator()
    {
        RuleFor(x => x.Gender).IsInEnum().WithMessage("Invalid gender value.");
    }
}
