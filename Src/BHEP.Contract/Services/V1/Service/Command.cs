using BHEP.Contract.Abstractions.Message;
using BHEP.Contract.Enumerations;
using Microsoft.AspNetCore.Http;

namespace BHEP.Contract.Services.V1.Service;
public class Command
{
    public record CreateServiceCommand(
        string Name,
        IFormFile Image,
        ServiceType Type,
        string Description,
        float Price,
        TimeExpired Duration) : ICommand<Responses.ServiceResponse>;


    public record UpdateServiceCommand(
        int Id,
        string Name,
        IFormFile Image,
        ServiceType Type,
        string Description,
        float Price,
        TimeExpired Duration) : ICommand<Responses.ServiceResponse>;



    public record DeleteServiceCommand(int Id) : ICommand;

}
