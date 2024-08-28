namespace BHEP.Domain.Exceptions;
public static class RoleException
{
    public class RoleBadRequestException : BadRequestException
    {
        public RoleBadRequestException(string message) : base(message)
        {
        }
    }

    public class RoleIdNotFoundException : NotFoundException
    {
        public RoleIdNotFoundException()
            : base($"Role not found.") { }
    }
}
