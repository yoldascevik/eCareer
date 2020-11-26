using System.Net;

namespace Career.Exceptions.Exceptions
{
    public class AlreadyExistsException: CareerExceptionBase
    {
        public AlreadyExistsException(string message)
        {
            StatusCode = (int)HttpStatusCode.BadRequest;
            Message = message;
        }

        public override string Message { get; }
    }
}