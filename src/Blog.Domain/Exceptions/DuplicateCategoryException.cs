using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Exceptions
{
    public class DuplicateCategoryException : Exception
    {
        public override string? StackTrace { get; }

        public DuplicateCategoryException() : base("Duplicate category") { }

        public DuplicateCategoryException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}
