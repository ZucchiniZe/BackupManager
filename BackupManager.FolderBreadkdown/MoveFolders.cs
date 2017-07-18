using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace BackupManager.FolderBreadkdown
{
    public class MoveFolders
    {
        /// <summary>
        /// Take a top level directory containing timestamped folders and distribute into a hierarchal tree based upon year and month
        /// </summary>
        /// <param name="sourcePath">the original path of folders to distribute</param>
        /// <param name="destPath">the path where folders are to be copied to in a tree</param>
        public static void Distribute(string sourcePath, string destPath)
        {
            // if the destination does not exist just skip
            if (!(Directory.Exists(destPath)))
                return;

            // go through and analyze the directories
            foreach (var path in Directory.EnumerateDirectories(sourcePath))
            {
                // try to parse the time from the foldername and then copy it's contents
                try
                {
                    var dir = new DirectoryInfo(path).Name;
                    var dirDate = dir.Split('T').First();

                    var dirTime = Convert.ToDateTime(dirDate);
                    
                    var monthDir = Path.Combine(destPath, dirTime.Year.ToString(), dirTime.Month.ToString());
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
        /// The inverse of Distribute, takes a tree dir which is a result of the distribute function and collapses into a flat folder structure.
        /// </summary>
        /// <param name="sourcePath">source path containing the distributed tree</param>
        /// <param name="destPath">where to output the flat folders</param>
        public static void Gather(string sourcePath, string destPath)
        {
            // only go 3 levels so we can get the folders with the log data
            // meaning that we don't have to know the contents of the folders
            List<string> dirs = GetFolders(sourcePath, 3);

            foreach (var currentDir in dirs)
            {
                var dirName = new DirectoryInfo(currentDir).Name;

                var endDir = Path.Combine(destPath, dirName);

                Directory.CreateDirectory(endDir);

                CopyFolder(currentDir, endDir);
            }
        }

        /// <summary>
        /// Copy the contents of a folder instead of moving it (DOES NOT recursively copy)
        /// </summary>
        /// <param name="sourcePath">the original location of the folder you would like to copy</param>
        /// <param name="destPath">the new location of the folder you want to move to</param>
        private static void CopyFolder(string sourcePath, string destPath)
        {
            if (Directory.Exists(sourcePath) && Directory.Exists(destPath))
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

        /// <summary>
        /// Recusrively get folders to a certain depth and only return the lowest depth of folders
        /// </summary>
        /// <param name="root">the root path to serch from</param>
        /// <param name="depth">the depth of the search (how many levels of directories do you want to go down)</param>
        /// <returns>a list of folder paths</returns>
        private static List<string> GetFolders(string root, int depth)
        {
            var folders = new List<string>();

            foreach (var dir in Directory.EnumerateDirectories(root))
            {
                // only get the lowest level of folders
                if (depth == 1)
                    folders.Add(dir);

                // recurseively call this function
                if (depth > 0)
                    folders.AddRange(GetFolders(dir, depth - 1));
            }

            return folders;
        }
    }
}
