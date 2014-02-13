using SendSMS.Common.Entities;

namespace SendSMS.Common.MessageDispatchers
{
    public interface IMessageDispatcher
    {
        void SendSMS(Job job);
    }
}