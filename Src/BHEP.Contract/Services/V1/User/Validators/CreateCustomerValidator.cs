using FluentValidation;

namespace BHEP.Contract.Services.V1.User.Validators;
public class CreateCustomerValidator : AbstractValidator<Command.CreateUserCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Invalid email format.");
        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Phone number is required.")
                                    .Matches(@"^[0-9]+$").WithMessage("Phone number must contain only digits.");
        RuleFor(x => x.Gender).IsInEnum().WithMessage("Invalid gender value.");
    }
}
