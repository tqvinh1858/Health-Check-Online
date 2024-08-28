using BHEP.Contract.Enumerations;
using static BHEP.Contract.Services.V1.ServiceRate.Responses;

namespace BHEP.Contract.Services.V1.Service;
public static class Responses
{
    public record ServiceResponse(
        int Id,
        string Image,
        string Name,
        ServiceType Type,
        string Description,
        float Price,
        TimeExpired Duration
    );

    public record ServiceGetAllResponse(
        int Id,
        string Image,
        string Name,
        ServiceType Type,
        string Description,
        float Price,
        TimeExpired Duration,
        float Rate
        );

    public record ServiceGetByIdResponse(
        int Id,
        string Image,
        string Name,
        ServiceType Type,
        string Description,
        float Price,
        TimeExpired Duration,
        float Rate,
        List<ServiceRateResponse> Rates
        );


}
