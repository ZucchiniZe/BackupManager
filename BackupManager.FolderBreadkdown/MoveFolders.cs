using System;
using System.IO;

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
            foreach (var dir in Directory.EnumerateDirectories(sourcePath))
            {
                // try to parse the directory name
                try
                {
                    var directoryTime = DateTime.Parse(dir);

                    // TODO: create the hierarchy for the folders
                } catch
                {
                    // TODO: handle the error of parsing failing
                }
            }
        }

        public static void Gather(string parentPath)
        {
            // TODO: implement the gather function
        }
    }
}
