namespace Blog.Domain.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public override string? StackTrace { get; }

        public DuplicateEmailException() : base("Duplicate email") { }

        public DuplicateEmailException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}
