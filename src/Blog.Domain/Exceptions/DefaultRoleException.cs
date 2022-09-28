using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Exceptions
{
    public class DefaultRoleException : Exception
    {
        public override string? StackTrace { get; }

        public DefaultRoleException() : base("Cannot change or delete the default role") { }

        public DefaultRoleException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}
