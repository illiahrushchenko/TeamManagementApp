namespace Application.Common.Exceptions;

public class UnauthorizedException : Exception
{
    public string[] Errors { get; init; }

    public UnauthorizedException(IEnumerable<string> errors)
        : base("One or more authorization errors occurred")
    {
        Errors = errors.ToArray();
    }

    public UnauthorizedException(string error)
        : this(new string[] { error })
    {
    }
}