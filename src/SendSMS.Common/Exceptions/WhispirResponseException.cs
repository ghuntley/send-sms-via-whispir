using System;
using System.Runtime.Serialization;

namespace SendSMS.Common.Exceptions
{
    [Serializable]
    public class WhispirResponseException : Exception
    {
        public WhispirResponseException()
        {
        }

        public WhispirResponseException(string message) : base(message)
        {
        }

        public WhispirResponseException(string message, Exception inner) : base(message, inner)
        {
        }

        protected WhispirResponseException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}