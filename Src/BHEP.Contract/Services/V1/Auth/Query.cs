using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.Auth;
public static class Query
{
    public record Login(string Email, string Password) : IQuery<Responses.UserAuthenticated>;
    public record Token(string AccessToken, string? RefreshToken, DateTime RefreshTokenExpiryTime) : IQuery<Responses.UserAuthenticated>;
}
