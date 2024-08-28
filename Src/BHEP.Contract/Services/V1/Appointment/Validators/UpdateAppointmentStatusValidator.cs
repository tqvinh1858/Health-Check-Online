using FluentValidation;

namespace BHEP.Contract.Services.V1.Appointment.Validators;
public sealed class UpdateAppointmentStatusValidator : AbstractValidator<Command.UpdateAppointmentStatusCommand>
{
    public UpdateAppointmentStatusValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("CustomerId > 0.")
            .When(x => x.CustomerId != null);

        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("EmployeeId > 0.")
            .When(x => x.EmployeeId != null);

        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Status must be valid Enum.");
    }
}
