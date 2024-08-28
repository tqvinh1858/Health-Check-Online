using System.ComponentModel.DataAnnotations;
using BHEP.Contract.Abstractions.Message;

namespace BHEP.Contract.Services.V1.ProductRate;
public static class Command
{
    public record CreateProductRateCommand(
        int UserId,
        int ProductId,
        int TransactionId,
        string? Reason,
        [Range(0,5)]
        float Rate) : ICommand<Responses.CreateProductResponse>;

    public record UpdateProductRateCommand(
        int? Id,
        string? Reason,
        [Range(0,5)]
        float Rate) : ICommand<Responses.ProductRateResponse>;

    public record DeleteProductRateCommand(int Id) : ICommand;
}
