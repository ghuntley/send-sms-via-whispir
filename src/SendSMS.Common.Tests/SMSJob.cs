using System;
using FluentAssertions;
using FluentValidation;
using NUnit.Framework;
using SendSMS.Common.Entities;

namespace SendSMS.Common.Tests
{
    [TestFixture]
    public class SMSJobTests : TestBase
    {
        [Test]
        public void CastingSMSJobToSMSShouldBeValid()
        {
            var job = new Job(ValidSMS);

            var results = (SMS) job;
            results.To.Should().Be(ValidSMS.To);
            results.Message.Should().Be(ValidSMS.Message);
        }

        [Test]
        public void SMSShouldBePopulatedByConstructor()
        {
            var test1 = new Job(ValidSMS);
            test1.SMS.Should().NotBeNull();
        }

        [Test]
        public void ShouldNotThrowExceptionWhenCastingSMSJobToSMS()
        {
            var job = new Job(ValidSMS);

            SMS cast;
            Action test = () => cast = (SMS) job;
            test.ShouldNotThrow();
        }

        [Test]
        public void ShouldNotThrowExceptionWhenSMSParamterIsValid()
        {
            Action test = () => new Job(ValidSMS);
            test.ShouldNotThrow();
        }

        [Test]
        public void ShouldThrowExceptionWhenSMSParamaterIsInvalid()
        {
            Action test = () => new Job(new SMS());
            test.ShouldThrow<ValidationException>();
        }
    }
}