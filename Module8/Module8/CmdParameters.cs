using CommandLine;
using CommandLine.Text;

namespace Module8
{
    public class CmdParameters
    {
        [Option('u', "url", Required = true, HelpText = "Base url")]
        public string Url { get; set; }

        [Option('d', "destinationPath", Required = true, HelpText = "Path to destination directory")]
        public string DestinationPath { get; set; }

        [Option('l', "level", DefaultValue = 0, HelpText = "Max deep level for site links analyze")]
        public int Level { get; set; }

        [Option('o', "otherDomains", DefaultValue = false, HelpText = "Can it switch to other domains")]
        public bool OtherDomains { get; set; }

        [Option('e', "validImageExtensions", Required = true, HelpText = "List of valid extensions, example: 'gif;png;jpg;jpeg'")]
        public string ValidImageExtensions { get; set; }

        [Option('s', "showMessage", DefaultValue = true, HelpText = "Show message about a current process")]
        public bool IsLog { get; set; }
     
        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this, current => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
