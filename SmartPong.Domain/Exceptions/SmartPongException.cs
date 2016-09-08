using System;

namespace SmartPong.Exceptions
{
    public class SmartPongException : Exception
    {
        public SmartPongException()
        {
            
        }

        public SmartPongException(string message) 
            : base(message)
        {
            
        }

        public SmartPongException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
