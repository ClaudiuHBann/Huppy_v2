using Shared.Responses;

namespace Shared.Exceptions
{
public class DatabaseException : BaseException
{
    public DatabaseException(ErrorResponse error) : base(error.Message)
    {
        Error = error;
    }

    public DatabaseException(ErrorResponse error, Exception inner) : base(error.Message, inner)
    {
        Error = error;
    }

    public ErrorResponse Error { get; }
}
}
