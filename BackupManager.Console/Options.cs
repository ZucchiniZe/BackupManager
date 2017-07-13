using CommandLine;

namespace BackupManager.Console
{
    [Verb("find-date", HelpText = "Find the first and last occuring date in generated csv logs.")]
    public class FindDateOptions
    {
        [Option('p', "prefix", Required = true, HelpText = "Prefix key to search for inside a file")]
        public string Prefix { get; set; }

        [Option('i', "inventory", Required = true, HelpText = "Path to inventory csv file")]
        public string InventoryFile { get; set; }
    }
}
