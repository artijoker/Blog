namespace Blog.Domain.Exceptions
{
    public class InvalidLoginException : Exception
    {
        public override string? StackTrace { get; }

        public InvalidLoginException() : base("Invalid login") { }

        public InvalidLoginException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}
