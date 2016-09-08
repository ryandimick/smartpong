using System;

namespace SmartPong.Exceptions
{
    /// <summary>
    /// 
    /// Represents errors that occur within SmartPong applications.
    /// 
    /// </summary>
    public class SmartPongException : Exception
    {
        /// <summary>
        /// 
        /// Initializes a new instance of the <see cref="SmartPongException"/> class.
        /// 
        /// </summary>
        public SmartPongException()
        {
            
        }

        /// <summary>
        /// 
        /// Initializes a new instance of the <see cref="SmartPongException"/> class with a specified error message.
        /// 
        /// </summary>
        /// 
        /// <param name="message">The message that describes the error.</param>
        public SmartPongException(string message) 
            : base(message)
        {
            
        }

        /// <summary>
        /// 
        /// Initiailizes a new instance of the <see cref="SmartPongException"/> class with a specified error message and a reference to the innter exception that is the cause of this exception.
        /// 
        /// </summary>
        /// 
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no exception is specified.</param>
        public SmartPongException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
