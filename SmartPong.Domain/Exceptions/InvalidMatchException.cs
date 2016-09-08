using System;

namespace SmartPong.Exceptions
{
    /// <summary>
    /// 
    /// Represents an error that occurred during match processing.
    /// 
    /// </summary>
    public sealed class InvalidMatchException : SmartPongException
    {
        /// <summary>
        /// 
        /// Initializes a new instance of the <see cref="InvalidMatchException"/> class.
        /// 
        /// </summary>
        public InvalidMatchException()
        {
            
        }

        /// <summary>
        /// 
        /// Initializes a new instance of the <see cref="InvalidMatchException"/> class with a specified error message.
        /// 
        /// </summary>
        /// 
        /// <param name="message">The message that describes the error.</param>
        public InvalidMatchException(string message)
            : base(message)
        {
            
        }

        /// <summary>
        /// 
        /// Initiailizes a new instance of the <see cref="InvalidMatchException"/> class with a specified error message and a reference to the innter exception that is the cause of this exception.
        /// 
        /// </summary>
        /// 
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no exception is specified.</param>
        public InvalidMatchException(string message, Exception innerException)
            : base(message, innerException)
        {
            
        }
    }
}
