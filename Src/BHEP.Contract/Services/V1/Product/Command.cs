using BHEP.Contract.Abstractions.Message;
using Microsoft.AspNetCore.Http;

namespace BHEP.Contract.Services.V1.Product;
public class Command { 
    public record CreateProductCommand(
        string Name,
        IFormFile Image,
        string Description,
        float Price,
        int Stock) : ICommand<Responses.ProductResponse>;


    public record UpdateProductCommand(
        int Id,
        string Name,
        IFormFile Image,
        string Description,
        float Price,
        int Stock) : ICommand<Responses.ProductResponse>;


    public record DeleteProductCommand(int Id) : ICommand;

}
