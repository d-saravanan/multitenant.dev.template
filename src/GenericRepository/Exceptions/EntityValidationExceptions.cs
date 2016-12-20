using System;
using System.Runtime.Serialization;

namespace MultiTenantRepository.Entities
{
    /// <summary>
    /// The entity validation exception
    /// </summary>
    public class EntityValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the System.Exception class.
        /// </summary>
        public EntityValidationException() { }

        /// <summary>
        /// Initializes a new instance of the System.Exception class. with the message
        /// </summary>
        /// <param name="message">the message</param>
        public EntityValidationException(string message) : base(message) { }

        /// <summary>
        /// Initializes a new instance of the System.Exception class with the message and the inner exception
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="innerException">the inner exception</param>
        public EntityValidationException(string message, Exception innerException) : base(message, innerException) { }

        /// <summary>
        /// Initializes a new instance of the System.Exception class with the serialization contexts
        /// </summary>
        /// <param name="info">The serialization information</param>
        /// <param name="context">The streaming context</param>
        protected EntityValidationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
