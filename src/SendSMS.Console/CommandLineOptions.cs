using System;
using System.Diagnostics;
using System.Reflection;
using CommandLine;
using CommandLine.Text;
using SendSMS.Common.Entities;
using SendSMS.Common.Helpers;

namespace SendSMS
{
    public class CommandLineOptions
    {
        [ParserState]
        public IParserState LastParserState { get; set; }

        [Option('t', "to", Required = true, HelpText = "Mobile phone number in international format.")]
        public string To { get; set; }


        [Option('m', "message", Required = true, HelpText = "Message to send.")]
        public string Message { get; set; }

        //[Option('v', "verbose", Required = false, HelpText = "Run verbosely.")]
        //public bool Verbose { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            string company = ReflectionHelper.GetAssemblyAttribute<AssemblyCompanyAttribute>(x => x.Company);
            string version =
                ReflectionHelper.GetAssemblyAttribute<AssemblyInformationalVersionAttribute>(x => x.InformationalVersion);
            string processname = Process.GetCurrentProcess().ProcessName;

            var help = new HelpText
            {
                AdditionalNewLineAfterOption = false,
                AddDashesToOption = true,
                Copyright = version,
                Heading = new CopyrightInfo(company, DateTime.Now.Year),
                MaximumDisplayWidth = 160,
            };

            help.AddPreOptionsLine(Environment.NewLine);
            help.AddPreOptionsLine(String.Format("Usage: {0} -t 61404654654 -m \"Howdy Geoff!\"", processname));

            help.AddOptions(this);

            if (LastParserState.Errors.Count <= 0) return help;

            string errors = help.RenderParsingErrorsText(this, 2); // indent with two spaces
            if (!string.IsNullOrEmpty(errors))
            {
                help.AddPostOptionsLine(Environment.NewLine);
                help.AddPostOptionsLine("ERROR(s):");
                help.AddPostOptionsLine(Environment.NewLine);
                help.AddPostOptionsLine(errors);
            }

            return help;
        }


        public static implicit operator SMS(CommandLineOptions options)
        {
            return new SMS
            {
                Message = options.Message,
                To = options.To
            };
        }
    }
}