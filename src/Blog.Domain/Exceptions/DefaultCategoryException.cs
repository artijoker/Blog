using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Exceptions
{
    public class DefaultCategoryException : Exception
    {
        public override string? StackTrace { get; }

        public DefaultCategoryException() : base("Cannot change or delete the default category")
        {
        }

        public DefaultCategoryException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}
