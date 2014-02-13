using System;

namespace SendSMS.Common.Entities
{
    [Serializable]
    public class WhispirSMS
    {
        public WhispirSMS()
        {
            subject = " ";
        }

        public string to { get; set; }
        public string body { get; set; }

        public string subject { get; private set; }

        public static implicit operator WhispirSMS(SMS sms)
        {
            return new WhispirSMS
            {
                body = sms.Message,
                to = sms.To
            };
        }
    }
}