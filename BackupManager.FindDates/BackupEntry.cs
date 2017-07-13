using System;
using System.Collections.Generic;

namespace BackupManager.FindDates
{
    public class BackupEntry
    {
        /// <summary>
        /// customer id
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// first date for which a backup exists
        /// </summary>
        public DateTime FirstDate { get; set; }

        /// <summary>
        /// last date for which a backup exists
        /// </summary>
        public DateTime LastDate { get; set; }

        public override int GetHashCode()
        {
            return CustomerId.GetHashCode();
        }

        /// <summary>
        /// Parses a line of the file. Doesn't return anything because it has to check if the dictionary already contains the customer and to compare the dates.
        /// </summary>
        /// <param name="prefix">the prefix for the entry to search for</param>
        /// <param name="dict">the dictionary storing the backupentries</param>
        /// <param name="line">the line of the file to search through</param>
        public static void ParseEntry(string prefix, Dictionary<string, BackupEntry> dict, string line)
        {
            var sections = line.Split('/');
            // check if contains prefix and has customer id
            if (sections[0] == prefix && sections.Length == 3 && sections[2] != "")
            {
                var unparsedDate = sections[1].Split('T')[0]; // get date without time
                var customerID = sections[2].Replace(".zip", ""); // remove the .zip from the end

                try
                {
                    var date = Convert.ToDateTime(unparsedDate);

                    // check to see if the value already exists inside the dictionary
                    BackupEntry entry;
                    if (dict.TryGetValue(customerID, out entry))
                    {
                        // if the current date in memory is greater than the lastdate on record replace it
                        var dateDifference = DateTime.Compare(date, entry.LastDate);
                        // if current date is greater than the date currently stored
                        if (dateDifference > 0)
                        {
                            entry.LastDate = date;
                            // the first date we encounter isn't always necesarrily the first date that occured
                        }
                        else if (dateDifference < 0)
                        {
                            entry.FirstDate = date;
                        }
                    }
                    else
                    {
                        var createdEntry = new BackupEntry
                        {
                            CustomerId = customerID,
                            FirstDate = date,
                            LastDate = date
                        };

                        dict.Add(customerID, createdEntry);
                    }
                }
                catch (FormatException ex)
                {
                    ; // just skip the line with a noop
                }
            }
        }
    }
}