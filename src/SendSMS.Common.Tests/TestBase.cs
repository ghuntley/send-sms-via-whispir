using SendSMS.Common.Entities;

namespace SendSMS.Common.Tests
{
    public class TestBase
    {
        public TestBase()
        {
            ValidSMS = new SMS
            {
                To = "0404654654",
                Message = "Hello Word"
            };

            ValidJob = new Job(ValidSMS);
        }

        public SMS ValidSMS { get; private set; }
        public Job ValidJob { get; private set; }
    }
}