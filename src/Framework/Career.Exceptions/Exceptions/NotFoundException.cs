using System.Net;

namespace Career.Exceptions.Exceptions
{
    public class NotFoundException : CareerExceptionBase
    {
        public NotFoundException(string message) : base(message)
        {
            StatusCode = (int) HttpStatusCode.NotFound;
        }
    }
}