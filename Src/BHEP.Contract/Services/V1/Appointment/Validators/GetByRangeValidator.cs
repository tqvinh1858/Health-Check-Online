using FluentValidation;

namespace BHEP.Contract.Services.V1.Appointment.Validators;
public sealed class GetByRangeValidator : AbstractValidator<Query.GetByRange>
{
    public GetByRangeValidator()
    {
        RuleFor(x => x.Range)
            .GreaterThan(0).WithMessage("Range > 0.");

        RuleFor(x => x.Latitude)
            .NotEmpty().WithMessage("Latitude is required.")
            .Must(BeAValidLatitude).WithMessage("Latitude value in [-90, 90].");

        RuleFor(x => x.Longitude)
            .NotEmpty().WithMessage("Longitude is required.")
            .Must(BeAValidLongitude).WithMessage("Longitude value in [-180, 180].");

        RuleFor(x => x.PageIndex)
            .GreaterThan(0).WithMessage("PageIndex > 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize > 0.");
    }

    private bool BeAValidLatitude(string latitude)
    {
        if (double.TryParse(latitude, out double lat))
        {
            return lat >= -90 && lat <= 90;
        }
        return false;
    }

    private bool BeAValidLongitude(string longitude)
    {
        if (double.TryParse(longitude, out double lng))
        {
            return lng >= -180 && lng <= 180;
        }
        return false;
    }
}
