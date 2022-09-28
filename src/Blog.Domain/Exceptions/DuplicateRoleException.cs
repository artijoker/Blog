using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Exceptions
{
    public class DuplicateRoleException : Exception
    {
        public override string? StackTrace { get; }

        public DuplicateRoleException() : base("Duplicate role") { }

        public DuplicateRoleException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}
