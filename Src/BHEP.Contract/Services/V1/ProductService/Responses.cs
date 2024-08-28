namespace BHEP.Contract.Services.V1.ProductService;
public static class Responses
{
    public record OutstandingResponse(
        int Id,
        string Name,
        string Image,
        float Price,
        float Rate,
        string Type
        );

    public record ProductServiceResponse(
        int Id,
        string Name,
        string Image,
        float Price,
        string Type);
}
