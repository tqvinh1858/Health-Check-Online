using BHEP.Contract.Enumerations;
using static BHEP.Contract.Services.V1.Specialist.Responses;

namespace BHEP.Contract.Services.V1.Post;
public static class Responses
{
    public record PostGetAllResponse(
       int Id,
       int UserId,
       string UserName,
       string Title,
       string Content,
       int Age,
       Gender Gender,
       PostStatus Status,
       List<SpecialistResponse> Specialists,
       int TotalLike,
       int TotalCommentAndReply
      );

    public record PostGetByIdResponse(
        int Id,
        int UserId,
        string Title,
        string Content,
        int Age,
        Gender Gender,
        PostStatus Status,
        List<CommentResponse> Comments,
        List<SpecialistResponse> Specialists,
        int TotalLike
        );
    public record PostResponse(
        int Id,
        int UserId,
        string Title,
        string Content,
        int Age,
        Gender Gender,
        PostStatus Status
        );



    public record CommentResponse(
      int Id,
      string Content);


}
