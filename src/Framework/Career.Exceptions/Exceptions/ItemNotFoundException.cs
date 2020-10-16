using System.Net;

namespace Career.Exceptions.Exceptions
{
    public class ItemNotFoundException: CareerExceptionBase
    {
        public ItemNotFoundException(object displayName)
        {
            StatusCode = (int)HttpStatusCode.NotFound;
            Message = $"Item \"{displayName}\" not found!";
        }

        public override string Message { get; }
    }
}