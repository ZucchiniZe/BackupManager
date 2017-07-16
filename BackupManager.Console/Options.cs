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

    [Verb("folder-breakdown", HelpText = "Categorizes folders into a tree hierarchy based on their name and date")]
    public class FolderBreakdownOptions
    {
        [Option('d', "distribute", HelpText = "Run the folder distributor. This or gather is required")]
        public bool Distrute { get; set; }
        
        [Option('g', "gather", HelpText = "Run the folder gatherer")]
        public bool Gather { get; set; }

        [Option('s', "source", Required = true, HelpText = "The source of where to look")]
        public string SourcePath { get; set; }

        [Option("dest", HelpText = "The destination of the distrubute function")]
        public string DestinationPath { get; set; }
    }
}
