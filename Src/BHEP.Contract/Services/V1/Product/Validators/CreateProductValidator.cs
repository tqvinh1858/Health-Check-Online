using FluentValidation;

namespace BHEP.Contract.Services.V1.Product.Validators;
public class CreateProductValidator : AbstractValidator<Command.CreateProductCommand>
{
    public CreateProductValidator()
    {
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Invalid Price value.");
    }
}
