using System.Net;

namespace Career.Exceptions.Exceptions;

public class ItemAlreadyExistsException: CareerExceptionBase
{
    public ItemAlreadyExistsException(string displayName)
    {
        StatusCode = (int)HttpStatusCode.BadRequest;
        Message = $"Item \"{displayName}\" already exist!";
    }

    public override string Message { get; }
}