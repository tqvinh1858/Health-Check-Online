using AutoMapper;
using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Abstractions.Shared;
using BHEP.Contract.Constants;
using BHEP.Contract.Enumerations;
using BHEP.Contract.Services.V1.User;
using BHEP.Domain.Abstractions.Repositories;
using BHEP.Domain.Exceptions;
using BHEP.Persistence;

namespace BHEP.Application.Usecases.V1.User.Commands;
public sealed class CreateUserCommandHandler : ICommandHandler<Command.CreateUserCommand, Responses.UserResponse>
{
    private readonly IUserRepository userRepository;
    private readonly ApplicationDbContext context;
    private readonly IGeoLocationRepository geoLocationRepository;
    private readonly IMapper mapper;

    public CreateUserCommandHandler(
        IUserRepository userRepository,
        ApplicationDbContext context,
        IGeoLocationRepository geoLocationRepository,
        IMapper mapper)
    {
        this.userRepository = userRepository;
        this.context = context;
        this.geoLocationRepository = geoLocationRepository;
        this.mapper = mapper;
    }
    public async Task<Result<Responses.UserResponse>> Handle(Command.CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existEmail = await userRepository.FindByEmailAsync(request.Email);
        if (existEmail != null)
            throw new UserException.UserBadRequestException("Email has been register!");
        // HashPassword
        var hashPassword = userRepository.HashPassword(request.Password);

        // Create Location
        var geolocation = new Domain.Entities.UserEntities.GeoLocation
        {
            Latitude = "",
            Longitude = "",
        };
        geoLocationRepository.Add(geolocation);
        context.SaveChanges();

        var Customer = new Domain.Entities.UserEntities.User
        {
            RoleId = (int)UserRole.Customer,
            GeoLocationId = geolocation.Id,
            FullName = request.FullName,
            Email = request.Email,
            HashPassword = hashPassword,
            PhoneNumber = request.PhoneNumber,
            Gender = request.Gender,
            IsActive = true,
            CreatedDate = TimeZones.SoutheastAsia,
        };

        try
        {
            userRepository.Add(Customer);
            await context.SaveChangesAsync();

            var response = mapper.Map<Responses.UserResponse>(Customer);
            return Result.Success(response, 201);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
