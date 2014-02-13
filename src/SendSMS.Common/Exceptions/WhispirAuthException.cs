using System;
using System.Runtime.Serialization;

namespace SendSMS.Common.Exceptions
{
    [Serializable]
    public class WhisipirAuthException : Exception
    {
        public WhisipirAuthException()
        {
        }

        public WhisipirAuthException(string message) : base(message)
        {
        }

        public WhisipirAuthException(string message, Exception inner) : base(message, inner)
        {
        }

        protected WhisipirAuthException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}