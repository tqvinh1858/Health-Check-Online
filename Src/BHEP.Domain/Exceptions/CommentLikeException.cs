using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHEP.Domain.Exceptions;
public static class CommentLikeException
{
    public class CommentLikeBadRequestException : BadRequestException
    {
        public CommentLikeBadRequestException(string message) : base(message)
        {
        }
    }

    public class CommentLikeNotFoundException : NotFoundException
    {
        public CommentLikeNotFoundException()
            : base($"CommentLike not found.") { }
    }
}
