using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using SendSMS.Common.Entities;

namespace SendSMS.Common.Tests
{
    [TestFixture]
    public class WhispirSMSTests : TestBase
    {
        [Test]
        public void ShouldHaveWhitespaceForSubjectToWorkaroundWhispirApiImplementation()
        {
            var whispirsms = new WhispirSMS();

            whispirsms.subject.Should().Be(" ");
        }
    }
}
