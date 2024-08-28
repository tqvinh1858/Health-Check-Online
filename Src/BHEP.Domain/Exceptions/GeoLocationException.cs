namespace BHEP.Domain.Exceptions;
public static class GeoLocationException
{
    public class GeoLocationBadRequestException : BadRequestException
    {
        public GeoLocationBadRequestException(string message) : base(message)
        {
        }
    }

    public class GeoLocationIdNotFoundException : NotFoundException
    {
        public GeoLocationIdNotFoundException()
            : base($"GeoLocation not found.") { }
    }
}
