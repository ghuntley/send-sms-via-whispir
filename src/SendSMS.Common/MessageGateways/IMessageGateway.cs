using SendSMS.Common.Entities;

namespace SendSMS.Common.MessageGateways
{
    public interface IMessageGateway
    {
        void SendSMS(SMS sms);
    }
}