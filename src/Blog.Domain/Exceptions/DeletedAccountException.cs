using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Exceptions
{
    public class DeletedAccountException : Exception
    {
        public override string? StackTrace { get; }

        public DeletedAccountException() : base("Account deleted") 
        { 
        }

        public DeletedAccountException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}
