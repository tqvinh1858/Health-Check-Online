using FluentValidation;

namespace BHEP.Contract.Services.V1.Product.Validators;
public class UpdateProductValidator : AbstractValidator<Command.UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Invalid Price value.");
    }
}
