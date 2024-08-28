using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHEP.Domain.Exceptions;
public static class CommentException
{
    public class CommentBadRequestException : BadRequestException
    {
        public CommentBadRequestException(string message) : base(message)
        {
        }
    }

    public class CommentNotFoundException : NotFoundException
    {
        public CommentNotFoundException()
            : base($"Comment not found.") { }
    }
}
