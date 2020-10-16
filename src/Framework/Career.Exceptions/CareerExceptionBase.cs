using System;

namespace Career.Exceptions
{
    public abstract class CareerExceptionBase : Exception, IStatusCodedException
    {
        public int StatusCode { get; set; }
    }
}