using System;
using System.IO;

namespace OpenInApp.Common.Helpers
{
    public class GeneralOptionsHelper
    {
        public static string GetActualPathToExe(string appParentFolderName, string appFolderName, string executableFileToBrowseFor)
        {
            var programFiles = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            var programFilesFolders = programFiles.Parent.GetDirectories(programFiles.Name.Replace(" (x86)", string.Empty) + "*");

            foreach (DirectoryInfo programFilesFolder in programFilesFolders)
            {
                var appParentFolderPaths = programFilesFolder.GetDirectories(appParentFolderName);
                foreach (DirectoryInfo appParentFolderPath in appParentFolderPaths)
                {
                    var appFolderPaths = appParentFolderPath.GetDirectories(appFolderName + "*");
                    foreach (DirectoryInfo appFolderPath in appFolderPaths)
                    {
                        var path = Path.Combine(appFolderPath.FullName, executableFileToBrowseFor);
                        if (File.Exists(path))
                        {
                            return path;
                        }
                    }
                }
            }

            return null;
        }
    }
}
