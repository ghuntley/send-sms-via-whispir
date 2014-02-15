using System;
using System.Diagnostics.Contracts;
using SendSMS.Common.Entities;

namespace SendSMS.Common.MessageGateways
{
    [ContractClass(typeof(MessageGatewayContracts))]
    public interface IMessageGateway
    {
        void SendSMS(SMS sms);
    }
}