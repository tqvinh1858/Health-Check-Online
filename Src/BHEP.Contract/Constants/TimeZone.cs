namespace BHEP.Contract.Constants;
public static class TimeZones
{
    public static readonly DateTime SoutheastAsia = TimeZoneInfo.ConvertTimeFromUtc(
        DateTime.UtcNow,
        TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"));
}
