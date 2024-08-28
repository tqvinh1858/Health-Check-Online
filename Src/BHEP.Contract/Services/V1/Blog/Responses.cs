using BHEP.Contract.Enumerations;

namespace BHEP.Contract.Services.V1.Blog;

public static class Responses
{
    public record BlogGetAllResponse(
        int Id,
        int UserId,
        string UserName,
        string Title,
        string Content,
        int View,
        string Image);

    public record BlogGetByIdResponse(
        int Id,
        int UserId,
        string UserName,
        string Title,
        string Content,
        int View,
        float Rate,
        List<TopicResponse> Topics,
        ICollection<PhotoResponse> Photos);

    public record BlogResponse(
        int Id,
        int UserId,
        string Title,
        string Content,
        BlogStatus Status,
        List<TopicResponse> Topics,
        List<PhotoResponse> Photos);

    public record TopicResponse(
        int Id,
        string Name);

    public record PhotoResponse(
        int Id,
        string Image,
        int ONum);
}
