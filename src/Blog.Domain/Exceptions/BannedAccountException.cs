using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Exceptions
{
    public class BannedAccountException : Exception
    {
        public override string? StackTrace { get; }

        public BannedAccountException() : base("Account is blocked") { }

        public BannedAccountException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}
