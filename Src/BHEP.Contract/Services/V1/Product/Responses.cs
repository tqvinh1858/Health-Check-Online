using static BHEP.Contract.Services.V1.ProductRate.Responses;

namespace BHEP.Contract.Services.V1.Product;
public static class Responses
{
    public record ProductResponse(
        int Id,
        string Name,
        string Image,
        string Description,
        float Price,
        int Stock
        );

    public record ProductGetAllResponse(
        int Id,
        string Name,
        string Image,
        string Description,
        float Price,
        int Stock,
        float Rate
        );

    public record ProductGetByIdResponse(
        int Id,
        string Name,
        string Image,
        string Description,
        float Price,
        int Stock,
        float Rate,
        List<ProductRateResponse> Rates
        );
}
