using System;

namespace SendSMS.Common.Entities
{
    [Serializable]
    public class SMS
    {
        /// <summary>
        ///     the phone number that the SMS will be sent to.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        ///     the message body of the SMS.
        /// </summary>
        public string Message { get; set; }
    }
}