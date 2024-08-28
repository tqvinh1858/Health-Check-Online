using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHEP.Domain.Exceptions;
public static class PostLikeException
{
    public class PostLikeBadRequestException : BadRequestException
    {
        public PostLikeBadRequestException(string message) : base(message)
        {
        }
    }

    public class PostLikeNotFoundException : NotFoundException
    {
        public PostLikeNotFoundException()
            : base($"PostLike not found.") { }
    }

    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException()
            : base("User not found") { }
    }

    public class PostNotFoundException : NotFoundException
    {
        public PostNotFoundException()
            : base($"Post not found.") { }
    }
}
