using System;
using System.IO;
using System.Collections.Generic;

namespace FindDates
{
    public class FindBackupDates
    {
        /// <summary>
        /// Parses a csv file for backup dates
        /// </summary>
        /// <param name="prefix">the prefix to search for</param>
        /// <param name="filePath">the path of the csv file to serach through</param>
        /// <returns>a dictionary with key of customer id and value of BackupEntry</returns>
        public static Dictionary<string, BackupEntry> ParseFile(string prefix, string filePath)
        {
            // instantiate a dictionary to pass into the parser
            // key: customer id
            // value: backupentry class
            var entries = new Dictionary<string, BackupEntry>();

            if (File.Exists(filePath))
            {
                foreach (var line in File.ReadLines(filePath))
                {
                    var sections = line.Split(',');
                    BackupEntry.ParseEntry(prefix, entries, sections[1].Replace("\"", ""));
                }
            }
            else
            {
                throw new ArgumentException($"{filePath} is not a vaild path to an inventory file");
            }

            return entries;
        }
    }
}
