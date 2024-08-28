using BHEP.Application.Authentications;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Services.V1.Auth;

namespace BHEP.Application.Usecases.V1.Auth.Queries;
public sealed class RefreshTokenQueryHandler : IQueryHandler<Query.Token, Responses.UserAuthenticated>
{
    private readonly IJWTTokenService jwtTokenService;

    public RefreshTokenQueryHandler(
        IJWTTokenService jwtTokenService)
    {
        this.jwtTokenService = jwtTokenService;
    }

    public async Task<Result<Responses.UserAuthenticated>> Handle(Query.Token request, CancellationToken cancellationToken)
    {
        if (request.RefreshTokenExpiryTime < TimeZones.SoutheastAsia)
            return Result.Failure<Responses.UserAuthenticated>("Expired Time", 401);

        try
        {
            var principle = jwtTokenService.GetPrincipalFromExpiredToken(request.AccessToken);

            var newAccessToken = jwtTokenService.GenerateAccessToken(principle.Claims);
            var newRefreshToken = jwtTokenService.GenerateRefreshToken();

            var response = new Responses.UserAuthenticated()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                RefreshTokenExpiryTime = TimeZones.SoutheastAsia.AddYears(1),
            };
            return Result.Success(response);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
