using System.Diagnostics.Contracts;
using FluentValidation;
using SendSMS.Common.Validators;

namespace SendSMS.Common.Entities
{
    public class Job
    {
        public Job(SMS sms) : this()
        {
            SMS = sms;
            SMSValidator.ValidateAndThrow(sms);
        }

        private Job()
        {
            SMSValidator = new SMSValidator();
        }

        public SMS SMS { get; private set; }

        public SMSValidator SMSValidator { get; private set; }

        public static implicit operator SMS(Job job)
        {
            Contract.Requires(job != null);

            return new SMS
            {
                Message = job.SMS.Message,
                To = job.SMS.To
            };
        }
    }
}