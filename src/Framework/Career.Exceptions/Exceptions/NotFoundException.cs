using System.Net;

namespace Career.Exceptions.Exceptions
{
    public class NotFoundException: CareerExceptionBase
    {
        public NotFoundException(string message)
        {
            StatusCode = (int)HttpStatusCode.NotFound;
            Message = message;
        }

        public override string Message { get; }
    }
}