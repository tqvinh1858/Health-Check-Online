using AutoMapper;
using BHEP.Application.Authentications;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Services.V1.Auth;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using static BHEP.Contract.Services.V1.User.Responses;

namespace BHEP.Application.Usecases.V1.Auth.Queries;
public sealed class LoginQueryHandler : IQueryHandler<Query.Login, Responses.UserAuthenticated>
{
    private readonly IJWTTokenService jwtTokenService;
    private readonly IUserRepository customerRepository;
    private readonly IRoleRepository roleRepository;
    private readonly IMapper mapper;
    public LoginQueryHandler(
        IJWTTokenService jwtTokenService,
        IUserRepository customerRepository,
        IRoleRepository roleRepository,
        IMapper mapper)
    {
        this.jwtTokenService = jwtTokenService;
        this.customerRepository = customerRepository;
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }

    public async Task<Result<Responses.UserAuthenticated>> Handle(Query.Login request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.FindByEmailAsync(request.Email)
            ?? throw new AuthException.AuthEmailNotFoundException();

        // Check Active
        if (customer.IsDeleted)
            throw new AuthException.AuthBadRequestException("Account is not active");

        // Check Password
        if (!customerRepository.VerifyPassword(customer.HashPassword, request.Password))
            throw new AuthException.AuthBadRequestException("Password Incorrect");

        try
        {
            var role = await roleRepository.FindByIdAsync(customer.RoleId);

            //Get Claims
            var claims = await customerRepository.GenerateClaims(customer, role.Name);

            var token = jwtTokenService.GenerateAccessToken(claims);
            var refreshToken = jwtTokenService.GenerateRefreshToken();

            var user = mapper.Map<UserResponse>(customer);
            var response = new Responses.UserAuthenticated()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = TimeZones.SoutheastAsia.AddYears(1),
                User = user,
            };

            return Result.Success(response);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }



}
