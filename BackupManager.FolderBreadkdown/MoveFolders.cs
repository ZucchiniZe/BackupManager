using System;
using System.IO;
using System.Linq;

namespace BackupManager.FolderBreadkdown
{
    public class MoveFolders
    {
        public static void Distribute(string sourcePath, string destinationPath)
        {
            // if the destination does not exist just skip
            if (!(Directory.Exists(destinationPath)))
                return;

            // go through and analyze the 
            foreach (var path in Directory.EnumerateDirectories(sourcePath))
            {
                // try to parse the directory name
                try
                {
                    var dir = path.Split('\\').Last();
                    var dirDate = dir.Split('T').First();

                    var directoryTime = Convert.ToDateTime(dirDate);

                    var yearDir = directoryTime.Year.ToString();
                    var monthDir = $"{destinationPath}{yearDir}\\{directoryTime.Month.ToString()}";

                    // create the month directory
                    // need to compose the year dir as well
                    Directory.CreateDirectory(monthDir);

                    Directory.Move(path, monthDir + "\\" + dir);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Please select a source directory that has folders with date names.");
                }
            }
        }

        public static void Gather(string sourcePath)
        {
            // TODO: implement the gather function
        }
    }
}
