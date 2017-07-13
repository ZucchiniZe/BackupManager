using System;
using System.Linq;
using System.Collections.Generic;
using CommandLine;
using BackupManager.FindDates;

namespace BackupManager.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<FindDateOptions>(args)
                .WithParsed<FindDateOptions>(options =>
                {
                    Dictionary<string, BackupEntry> entries = FindBackupDates.ParseFile(options.Prefix, options.InventoryFile);

                    // generate csv output
                    foreach (BackupEntry entry in entries.Values.OrderBy(e => e.CustomerId))
                    {
                        System.Console.WriteLine($"\"{entry.CustomerId}\",\"{entry.FirstDate.ToString("d")}\",\"{entry.LastDate.ToString("d")}\"");
                    }
                });
        }
    }
}