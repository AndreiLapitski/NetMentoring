using System;
using SimpleDownloader;

namespace Module8
{
    class Program
    {
        static void Main(string[] args)
        {
            CmdParameters cmdParameters = new CmdParameters();
            if (CommandLine.Parser.Default.ParseArguments(args, cmdParameters))
            {
                Downloader downloader = new Downloader(
                    cmdParameters.DestinationPath,
                    cmdParameters.Level,
                    cmdParameters.OtherDomains,
                    cmdParameters.ValidImageExtensions,
                    cmdParameters.IsLog);

                downloader.Download(new Uri(cmdParameters.Url));
            }     
        }
    }
}
