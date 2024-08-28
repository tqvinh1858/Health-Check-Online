namespace BHEP.Contract.Services.V1.BlogRate;
public static class Responses
{
    public record BlogRateResponse(
        int Id,
        int UserId,
        int BlogId,
        float Rate);
}
