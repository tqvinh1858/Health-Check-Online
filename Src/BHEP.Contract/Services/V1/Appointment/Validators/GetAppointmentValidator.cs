using FluentValidation;

namespace BHEP.Contract.Services.V1.Appointment.Validators;
public sealed class GetAppointmentValidator : AbstractValidator<Query.GetAppointmentQuery>
{
    public GetAppointmentValidator()
    {
        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId > 0 if provided.")
            .When(x => x.UserId.HasValue);

        RuleFor(x => x.PageIndex)
            .GreaterThan(0).WithMessage("PageIndex > 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize > 0.");
    }
}
