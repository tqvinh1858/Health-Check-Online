using Asp.Versioning;
using BHEP.Presentation.Abstractions;
using MediatR;

namespace BHEP.Presentation.Controller.V1;
[ApiVersion("1")]
public class CodeController : ApiController
{
    public CodeController(ISender sender) : base(sender)
    {
    }
}
