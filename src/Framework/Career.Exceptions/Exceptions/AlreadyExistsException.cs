using System.Net;

namespace Career.Exceptions.Exceptions;

public class AlreadyExistsException : CareerExceptionBase
{
    public AlreadyExistsException(string message) : base(message)
    {
        StatusCode = (int) HttpStatusCode.BadRequest;
    }
}