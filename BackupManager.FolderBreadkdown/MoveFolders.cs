using System;
using System.IO;
using System.Linq;

namespace BackupManager.FolderBreadkdown
{
    public class MoveFolders
    {
        /// <summary>
        /// Take a top level directory containing timestamped folders and distribute into a hierarchal tree based upon year and month
        /// </summary>
        /// <param name="sourcePath">the original path of folders to distribute</param>
        /// <param name="destinationPath">the path where folders are to be copied to in a tree</param>
        public static void Distribute(string sourcePath, string destinationPath)
        {
            // if the destination does not exist just skip
            if (!(Directory.Exists(destinationPath)))
                return;

            // go through and analyze the directories
            foreach (var path in Directory.EnumerateDirectories(sourcePath))
            {
                // try to parse the time from the foldername and then copy it's contents
                try
                {
                    var dir = path.Split('\\').Last();
                    var dirDate = dir.Split('T').First();

                    var dirTime = Convert.ToDateTime(dirDate);
                    
                    var monthDir = Path.Combine(destinationPath, dirTime.Year.ToString(), dirTime.Month.ToString());
                    var endDir = Path.Combine(monthDir, dir);

                    // create all the dirs!
                    Directory.CreateDirectory(endDir);

                    // instead of doing the easy thing and just moving the files, i need to do what's right and copy them.
                    CopyFolder(path, endDir);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Please select a source directory that has folders with date names.");
                }
            }
        }

        /// <summary>
        /// Copy the contents of a folder instead of moving it (does not recursively copy)
        /// </summary>
        /// <param name="sourcePath">the original location of the folder you would like to copy</param>
        /// <param name="destPath">the new location of the folder you want to move to</param>
        private static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath))
            {
                var files = Directory.GetFiles(sourcePath);

                foreach (var file in files)
                {
                    var fileName = Path.GetFileName(file);
                    var destFile = Path.Combine(destPath, fileName);

                    File.Copy(file, destFile, overwrite: true);
                }
            }
        }

        public static void Gather(string sourcePath)
        {
            // TODO: implement the gather function
        }
    }
}
