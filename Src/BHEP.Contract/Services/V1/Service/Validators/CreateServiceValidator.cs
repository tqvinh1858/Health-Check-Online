using FluentValidation;

namespace BHEP.Contract.Services.V1.Service.Validators;
public class CreateServiceValidator : AbstractValidator<Command.CreateServiceCommand>
{
    public CreateServiceValidator()
    {
        RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid Type value.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Invalid Price value.");
        RuleFor(x => x.Duration).IsInEnum().WithMessage("Invalid Duration value.");
    }
}
