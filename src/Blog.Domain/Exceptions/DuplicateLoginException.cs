namespace Blog.Domain.Exceptions
{
    public class DuplicateLoginException : Exception
    {
        public override string? StackTrace { get; }

        public DuplicateLoginException() : base("Duplicate login") { }

        public DuplicateLoginException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}
