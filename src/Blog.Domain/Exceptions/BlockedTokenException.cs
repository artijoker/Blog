using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Exceptions
{
    public class BlockedTokenException : Exception
    {
        public override string? StackTrace { get; }

        public BlockedTokenException() : base("Blocked token") { }

        public BlockedTokenException(string message) : base(message) { }

        public BlockedTokenException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}

