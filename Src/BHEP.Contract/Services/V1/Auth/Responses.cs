using static BHEP.Contract.Services.V1.User.Responses;

namespace BHEP.Contract.Services.V1.Auth;
public static class Responses
{
    public class UserAuthenticated
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
        public UserResponse User { get; set; }
    }
}
