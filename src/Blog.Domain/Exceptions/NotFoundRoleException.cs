namespace Blog.Domain.Exceptions;

public class NotFoundRoleException : Exception
{
    public override string? StackTrace { get; }

    public NotFoundRoleException() : base("Not found role")
    {
    }

    public NotFoundRoleException(string? message, string? stackTrace = null) : base(message) => StackTrace = stackTrace;
}