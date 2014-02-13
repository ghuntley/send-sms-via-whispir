using System.Configuration;
using System.Reflection;

namespace SendSMS
{
    public static class AppConfig
    {
        public static string ApplicationName = Assembly.GetCallingAssembly().GetName().Name;
        public static string ApplicationVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public static string WhispirApiKey = ConfigurationManager.AppSettings["WhispirApiKey"];
        public static string WhispirApiUrl = ConfigurationManager.AppSettings["WhispirApiUrl"];
        public static string WhispirAuthorization = ConfigurationManager.AppSettings["WhispirApiAuthorization"];
    }
}