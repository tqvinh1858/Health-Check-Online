namespace BHEP.Contract.Services.V1.Role;

public static class Responses
{
    public record RoleResponse(
        int Id,
        string Name,
        string Description);

    public class RoleCacheResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public RoleCacheResponse(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
