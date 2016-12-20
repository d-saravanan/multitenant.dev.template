using System;
using System.Runtime.Serialization;

namespace MultiTenantServices.ServiceExceptions
{
    /// <summary>
    /// The entity pre processor exception
    /// </summary>
    public class EntityPreProcessorExceptions : Exception
    {
        /// <summary>
        /// intializes an instance of the entity pre processor exception
        /// </summary>
        public EntityPreProcessorExceptions() { }
        /// <summary>
        /// initializes an instance of The entity pre processor exception with message
        /// </summary>
        /// <param name="message">The message</param>
        public EntityPreProcessorExceptions(string message) : base(message) { }
        /// <summary>
        /// Initializes an instance of The entity pre processor exception with a message and an inner exception
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="innerException">The inner exception</param>
        public EntityPreProcessorExceptions(string message, Exception innerException) : base(message, innerException) { }
        /// <summary>
        /// Initializes and instance of The entity pre processor exception with the serialization info and streaming contexts
        /// </summary>
        /// <param name="info">the serialization information</param>
        /// <param name="context">the streaming context</param>
        protected EntityPreProcessorExceptions(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
