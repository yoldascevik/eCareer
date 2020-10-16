using System.Net;

namespace Career.Extensions.Web
{
    public static class HttpExtension
    {
        public static int ToInt32(this HttpStatusCode statusCode) 
            => (int)statusCode;
    }
}
