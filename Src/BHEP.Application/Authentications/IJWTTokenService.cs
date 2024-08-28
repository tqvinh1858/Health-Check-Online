using System.Security.Claims;

namespace BHEP.Application.Authentications;
public interface IJWTTokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}
