using System.Net;

namespace Career.Exceptions.Exceptions;

public class BusinessException : CareerExceptionBase
{
    public BusinessException(string message) : base(message)
    {
        StatusCode = (int) HttpStatusCode.BadRequest;
    }
}