using System;

namespace SmartPong.Exceptions
{
    public sealed class InvalidMatchException : SmartPongException
    {
        public InvalidMatchException()
        {
            
        }

        public InvalidMatchException(string message)
            : base(message)
        {
            
        }

        public InvalidMatchException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
