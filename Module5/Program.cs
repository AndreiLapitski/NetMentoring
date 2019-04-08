using System;
using System.Configuration;
using Task5.Configuration;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;
using System.Text;
using Task5.Resources;
using System.Linq;

namespace Task5
{
    public class Program
    {
        static void Main(string[] args)
        {
            void OnChanged(object obj, FileSystemEventArgs e)
            {
                CustomConfigurationSection configurationSection = ConfigurationManager.GetSection("customConfiguration") as CustomConfigurationSection;
                CurrentCulture culture = configurationSection.Culture;
                Thread.CurrentThread.CurrentCulture = culture.Culture;
                Thread.CurrentThread.CurrentUICulture = culture.Culture;

                IEnumerable<RuleElement> allRulesForTheDestinationFolder =
                    from RuleElement rule in configurationSection.Rules
                    where rule.DestinationFolder.Equals(Path.GetDirectoryName(e.FullPath))
                    select rule;

                int step = 0;
                foreach (RuleElement rule in allRulesForTheDestinationFolder)
                {
                    StringBuilder message = new StringBuilder();
                    int lastNumber = 1;
                    string originalFileName = Path.GetFileName(e.FullPath);
                    string modifiedFileName = originalFileName;
                    string absolutePathWithModifiedFileName = string.Empty;

                    if (Regex.IsMatch(Path.GetFileNameWithoutExtension(e.FullPath), rule.Pattern))
                    {
                        Console.WriteLine(Resource.Msg_RuleWorks, rule.Id);

                        if (rule.AddEnumeration)
                        {
                            if (Directory.GetFiles(rule.DestinationFolder).Length > 1)
                            {
                                string[] allFileNames = Directory.GetFiles(rule.DestinationFolder);
                                Array.Sort(allFileNames);
                                string lastNumberedFileName = Path.GetFileName(allFileNames[allFileNames.Length - 2]);
                                Match match = Regex.Match(lastNumberedFileName, "^[0-9]+");
                                if (match.Success)
                                {
                                    lastNumber = Convert.ToInt32(match.Value);
                                    lastNumber++;
                                }
                            }

                            modifiedFileName = originalFileName.Insert(0, string.Concat(lastNumber, ") "));
                        }

                        if (rule.AddTransferDate)
                        {
                            modifiedFileName = modifiedFileName.Insert(modifiedFileName.IndexOf('.'),
                                    string.Format(Resource.Msg_WasAdded, DateTime.Now.ToLongDateString()));
                        }

                        if (rule.AddEnumeration == false && rule.AddTransferDate == false)
                        {
                            message.AppendFormat(Resource.Msg_FileCreatedOnThePath,
                                originalFileName,
                                rule.DestinationFolder);
                        }

                        absolutePathWithModifiedFileName = Path.Combine(rule.DestinationFolder, modifiedFileName);
                        File.Move(e.FullPath, absolutePathWithModifiedFileName);

                        message.AppendFormat(Resource.Msg_FileCreatedOnThePath,
                            modifiedFileName,
                            rule.DestinationFolder);
                        Console.WriteLine(message.ToString());
                    }
                    else
                    {
                        step++;
                        if (step == allRulesForTheDestinationFolder.Count())
                        {
                            Console.WriteLine(Resource.Msg_NoRuleFound,
                               e.Name,
                               configurationSection.Rules.DefaultFolder);

                            File.Move(e.FullPath, Path.Combine(configurationSection.Rules.DefaultFolder, e.Name));
                        }
                    }
                }
            }

            CustomConfigurationSection customConfigurationSection = ConfigurationManager.GetSection("customConfiguration") as CustomConfigurationSection;
            CurrentCulture currentCulture = customConfigurationSection.Culture;
            Thread.CurrentThread.CurrentCulture = currentCulture.Culture;
            Thread.CurrentThread.CurrentUICulture = currentCulture.Culture;

            List<string> listeningFolders = new List<string>();
            foreach (FolderElement element in customConfigurationSection.ListeningFolders)
            {
                string absolutePath = Path.Combine(element.DestinationPath, element.FolderName);

                listeningFolders.Add(absolutePath);

                if (!Directory.Exists(absolutePath))
                {
                    Directory.CreateDirectory(absolutePath);
                }
            }
            if (!Directory.Exists(customConfigurationSection.Rules.DefaultFolder))
            {
                Directory.CreateDirectory(customConfigurationSection.Rules.DefaultFolder);
            }

            List<FileSystemWatcher> fileSystemWatchers = new List<FileSystemWatcher>();
            for (int i = 0; i < listeningFolders.Count; i++)
            {
                fileSystemWatchers.Add(new FileSystemWatcher());
                fileSystemWatchers[i].Path = listeningFolders[i];
                fileSystemWatchers[i].NotifyFilter = NotifyFilters.FileName;
                fileSystemWatchers[i].Created += OnChanged;
                fileSystemWatchers[i].EnableRaisingEvents = true;
            }

            Console.WriteLine(Resource.Msg_ApplicationStarted);
            Thread.Sleep(1000000);
        }
    }
}
