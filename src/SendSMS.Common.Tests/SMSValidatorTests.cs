using FluentValidation.TestHelper;
using NUnit.Framework;
using SendSMS.Common.Entities;
using SendSMS.Common.Validators;

namespace SendSMS.Common.Tests
{
    [TestFixture]
    public class SMSValidatorTests
    {
        [SetUp]
        public void Setup()
        {
            Validator = new SMSValidator();
        }

        private SMSValidator Validator;

        [Test]
        public void ShouldHaveValidationErrorWhenMessageToIsNullOrEmptyOrWhitespace()
        {
            Validator.ShouldHaveValidationErrorFor(sms => sms.Message, null as string);
            Validator.ShouldHaveValidationErrorFor(sms => sms.Message, " ");
            Validator.ShouldHaveValidationErrorFor(sms => sms.Message, "");
        }

        [Test]
        public void ShouldHaveValidationErrorWhenToIsInvalid()
        {
            Validator.ShouldHaveValidationErrorFor(sms => sms.To, "hello world");
            Validator.ShouldHaveValidationErrorFor(sms => sms.To, "12345");
        }

        [Test]
        public void ShouldHaveValidationErrorWhenToIsNullOrEmptyOrWhitespace()
        {
            Validator.ShouldHaveValidationErrorFor(sms => sms.To, null as string);
            Validator.ShouldHaveValidationErrorFor(sms => sms.To, " ");
            Validator.ShouldHaveValidationErrorFor(sms => sms.To, "");
        }

        [Test]
        public void ShouldNotHaveValidationErrorWhenToIsValid()
        {
            Validator.ShouldNotHaveValidationErrorFor(sms => sms.To, "+61404654654");
            Validator.ShouldNotHaveValidationErrorFor(sms => sms.To, "61404654654");
            Validator.ShouldNotHaveValidationErrorFor(sms => sms.To, "0404654654");
        }
    }
}