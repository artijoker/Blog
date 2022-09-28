namespace Blog.Domain.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public override string? StackTrace { get; }

        public InvalidPasswordException() : base("Invalid password") { }

        public InvalidPasswordException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
    }
}
