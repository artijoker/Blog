using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Exceptions
{
    public class OperationLockException : Exception
    {
        public override string? StackTrace { get; }

        public OperationLockException() : base("Operation blocked") { }

        public OperationLockException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}
