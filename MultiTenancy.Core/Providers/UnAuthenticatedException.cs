using System;
using System.Runtime.Serialization;

namespace MultiTenancy.Core.Providers
{
    [Serializable]
    internal class UnAuthenticatedException : Exception
    {
        public UnAuthenticatedException()
        {
        }

        public UnAuthenticatedException(string message) : base(message)
        {
        }

        public UnAuthenticatedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UnAuthenticatedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}