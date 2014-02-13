using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SendSMS.Common.MessageGateways;

namespace SendSMS.Common.Tests
{
    [TestFixture]
    public class WhispirGatewayTests
    {
        [Test]
        public void ConstructorShouldPopulateProperties()
        {
            const string auth = "helloworld";
            const string url = "http://localhost";
            const string key = "1234";

            var gateway = new WhispirGateway(auth, url, key);

            gateway.ApiAuthorization.Should().Be(auth);
            gateway.ApiBaseUrl.Should().Be(url);
            gateway.ApiKey.Should().Be(key);
        }

        [Test]
        public void ContentTypeShouldBeCorrect()
        {
            const string auth = "helloworld";
            const string url = "http://localhost";
            const string key = "1234";

            var gateway = new WhispirGateway(auth, url, key);

            const string expected = "application/vnd.whispir.message-v1+json";

            gateway.ContentType.Should().Be(expected);
        }
    }
}
