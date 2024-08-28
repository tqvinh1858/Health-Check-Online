namespace BHEP.Contract.Services.V2.Role;

public static class Responses
{
    public record RoleResponse(
        int Id,
        string Name,
        string Description);
}
