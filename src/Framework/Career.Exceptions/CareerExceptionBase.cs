using System;

namespace Career.Exceptions
{
    public abstract class CareerExceptionBase : Exception, IStatusCodedException
    {
        protected CareerExceptionBase()
        {
            
        }

        protected CareerExceptionBase(string message): base(message)
        {
            
        }
        
        public int StatusCode { get; set; }
    }
}