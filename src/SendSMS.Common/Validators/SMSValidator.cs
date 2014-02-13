using FluentValidation;
using PhoneNumbers;
using SendSMS.Common.Entities;

namespace SendSMS.Common.Validators
{
    public class SMSValidator : AbstractValidator<SMS>
    {
        public SMSValidator()
        {
            RuleFor(sms => sms.To)
                .NotEmpty()
                .Must(BeAValidPhoneNumber)
                .WithMessage("Please specify a valid phone numner");
            RuleFor(sms => sms.Message).NotEmpty();
        }

        public bool BeAValidPhoneNumber(string phonenumber)
        {
            PhoneNumberUtil phoneNumberUtil = PhoneNumberUtil.GetInstance();

            return phoneNumberUtil.IsPossibleNumber(phonenumber, "AU");
        }
    }
}