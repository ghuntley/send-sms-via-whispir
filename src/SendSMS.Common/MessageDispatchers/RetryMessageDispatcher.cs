using System;
using NLog;
using SendSMS.Common.Entities;
using SendSMS.Common.Exceptions;
using SendSMS.Common.Helpers;
using SendSMS.Common.MessageGateways;

namespace SendSMS.Common.MessageDispatchers
{
    public class RetryMessageDispatcher : IMessageDispatcher
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        public RetryMessageDispatcher(IMessageGateway gateway)
        {
            MessageGateway = gateway;
        }

        public IMessageGateway MessageGateway { get; private set; }

        public void SendSMS(Job job)
        {
            try
            {
                Retry.Do(() =>
                {
                    Log.Info("Sending SMS to message gateway.");
                    MessageGateway.SendSMS(job);
                }, TimeSpan.FromSeconds(6), 10);
            }
            catch
            {
                throw new MessageDispatchFailureException("Non-recoverable failure occurred whilst sending the SMS.");
            }
        }
    }
}