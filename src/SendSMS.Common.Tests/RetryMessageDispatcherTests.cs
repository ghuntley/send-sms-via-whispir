using System;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using SendSMS.Common.Exceptions;
using SendSMS.Common.MessageDispatchers;
using SendSMS.Common.MessageGateways;

namespace SendSMS.Common.Tests
{
    [TestFixture]
    public class RetryMessageDispatcherTests : TestBase
    {
        [Test]
        public void ShouldAttemptTenRetriesBeforeGivingUp()
        {
            var gateway = Substitute.For<IMessageGateway>();
            var dispatcher = new RetryMessageDispatcher(gateway);

            int count = 0;
            gateway.WhenForAnyArgs(x => x.SendSMS(null))
                .Do(x =>
                {
                    count++;
                    throw new Exception();
                });

            try
            {
                dispatcher.SendSMS(ValidJob);
            }
            catch (MessageDispatchFailureException)
            {
            }

            count.Should().Be(10);
        }

        [Test]
        public void ShouldThrowExceptionWhenGivingUp()
        {
            var gateway = Substitute.For<IMessageGateway>();
            var dispatcher = new RetryMessageDispatcher(gateway);

            gateway.WhenForAnyArgs(x => x.SendSMS(null))
                .Do(x =>
                {
                    throw new Exception();
                });

            Action test = () => dispatcher.SendSMS(ValidJob);
            test.ShouldThrow<MessageDispatchFailureException>();
        }
    }
}