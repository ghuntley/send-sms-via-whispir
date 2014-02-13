using System;
using System.Diagnostics;
using CommandLine;
using NLog;
using SendSMS.Common.Entities;
using SendSMS.Common.MessageDispatchers;
using SendSMS.Common.MessageGateways;
using ServiceStack;

namespace SendSMS
{
    /// <summary>
    ///     The exit/return code (aka %ERRORLEVEL%) on application exit.
    /// </summary>
    public enum ExitCode
    {
        Success = 0,
        Error = 1
    }

    internal class Program
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();
        private static IMessageGateway Gateway { get; set; }
        private static IMessageDispatcher Dispatcher { get; set; }

        private static void Main(string[] args)
        {
            var options = new CommandLineOptions();

            // allow app to be debugged in visual studio.
            if (Debugger.IsAttached)
            {
                args = "-t 0404654654 -m http://ghuntley.co/1cxNHP7 ".Split(' ');
            }

            // Parse in 'strict mode'; i.e. success or quit
            if (Parser.Default.ParseArgumentsStrict(args, options))
            {
                try
                {
                    Log.Trace("Results of parsing command line arguments: {0}", options.ToJson());

                    Gateway = new WhispirGateway(
                        AppConfig.WhispirAuthorization,
                        AppConfig.WhispirApiUrl,
                        AppConfig.WhispirApiKey);

                    Dispatcher = new RetryMessageDispatcher(Gateway);

                    var job = new Job(options);

                    Dispatcher.SendSMS(job);

                    Environment.Exit((int) ExitCode.Success);
                }
                catch (Exception ex)
                {
                    Log.Error(ex);
                }
            }

            Environment.Exit((int) ExitCode.Error);
        }
    }
}