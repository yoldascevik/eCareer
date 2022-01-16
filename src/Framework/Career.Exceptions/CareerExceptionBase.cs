using System.Net;

namespace Career.Exceptions;

public abstract class CareerExceptionBase : Exception, IStatusCodedException
{
    protected CareerExceptionBase()
    {
    }

    protected CareerExceptionBase(string message) : base(message)
    {
    }

    protected CareerExceptionBase(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }

    protected CareerExceptionBase(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = (int) statusCode;
    }

    public int StatusCode { get; set; } = 500;
}