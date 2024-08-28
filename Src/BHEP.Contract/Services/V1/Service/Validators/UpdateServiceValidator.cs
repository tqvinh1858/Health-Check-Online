using FluentValidation;

namespace BHEP.Contract.Services.V1.Service.Validators;
public class UpdateServiceValidator : AbstractValidator<Command.UpdateServiceCommand>
{
    public UpdateServiceValidator()
    {
        RuleFor(x => x.Type).IsInEnum().WithMessage("Invalid type value.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Invalid price value.");
        RuleFor(x => x.Duration).IsInEnum().WithMessage("Invalid duration value.");
    }
}
