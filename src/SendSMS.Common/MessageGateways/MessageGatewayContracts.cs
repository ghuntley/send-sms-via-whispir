using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SendSMS.Common.Entities;

namespace SendSMS.Common.MessageGateways
{
    [ContractClassFor(typeof(IMessageGateway))]
    abstract class MessageGatewayContracts : IMessageGateway
    {
        public void SendSMS(SMS sms)
        {
            Contract.Requires(!String.IsNullOrWhiteSpace(sms.To));
            Contract.Requires(!String.IsNullOrWhiteSpace(sms.Message));
        }
    }

}
