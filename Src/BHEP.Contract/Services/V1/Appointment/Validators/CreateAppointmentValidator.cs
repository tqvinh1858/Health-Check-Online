using System.Globalization;
using BHEP.Contract.Constants;
using FluentValidation;

namespace BHEP.Contract.Services.V1.Appointment.Validators;
public sealed class CreateAppointmentValidator : AbstractValidator<Command.CreateAppointmentCommand>
{
    public CreateAppointmentValidator()
    {
        RuleFor(x => x.CustomerId)
            .GreaterThan(0).WithMessage("CustomerId > 0.");

        RuleFor(x => x.EmployeeId)
            .GreaterThan(0).WithMessage("EmployeeId > 0.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .Must(BeAValidDate).WithMessage("Date formart: dd-MM-yyyy.")
            .Must(BeRatherThanDateTimeNow).WithMessage("Date must rather than DateTimeNow");

        RuleFor(x => x.Time)
            .NotEmpty().WithMessage("Time is required.")
            .Must(BeAValidTimeRange).WithMessage("Time formart: HH:mm - HH:mm.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price > 0.");

        RuleFor(x => x.Symptoms)
            .NotEmpty().WithMessage("Symptoms list must not be empty.")
            .Must(symptoms => symptoms.TrueForAll(symptom => symptom > 0)).WithMessage("All symptoms > 0.");
    }

    private bool BeAValidDate(string date)
    {
        return DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
    private bool BeRatherThanDateTimeNow(string date)
    {
        if (!DateTime.TryParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime scheduleDate))
            return false;

        if (scheduleDate < TimeZones.SoutheastAsia)
            return false;
        return true;
    }

    private bool BeAValidTimeRange(string timeRange)
    {
        string[] times = timeRange.Split(" - ");
        if (times.Length != 2)
            return false;

        return DateTime.TryParseExact(times[0], "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _) &&
               DateTime.TryParseExact(times[1], "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
    }
}
