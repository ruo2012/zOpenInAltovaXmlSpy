using EnvDTE;
using EnvDTE80;
using OpenInXxx.Dtos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenInXxx.Helpers
{
    public static class FileHelper
    {
        public static bool AreTypicalFileExtensions(IEnumerable<string> fullFileNames, IEnumerable<string> typicalFileExtension)
        {
            var result = false;

            var fileTypeExtensions = GetFileTypeExtensions(fullFileNames);

            foreach (var fileTypeExtension in fileTypeExtensions)
            {
                if (!string.IsNullOrEmpty(fileTypeExtension))
                {
                    result = typicalFileExtension.Contains(fileTypeExtension.TrimStart('.'));
                }
            }

            return result;
        }

        internal static IEnumerable<string> GetFileTypeExtensions(IEnumerable<string> fullFileNames)
        {
            var result = new List<string>();

            foreach (var fullFileName in fullFileNames)
            {
                result.Add(Path.GetExtension(fullFileName));
            }

            return result;
        }

        public static bool DoesFileExist(string fullFileName)
        {
            return DoFilesExist(new List<string> { fullFileName });
        }

        public static bool DoFilesExist(IEnumerable<string> fullFileNames)
        {
            var result = true;

            foreach (var fullFileName in fullFileNames)
            {
                if (!string.IsNullOrEmpty(fullFileName))
                {
                    if (!File.Exists(fullFileName))
                    {
                        result = false;
                    }
                }
            }

            return result;
        }

        public static string GetMissingFileName(IEnumerable<string> fullFileNames)
        {
            var result = string.Empty;

            foreach (var fullFileName in fullFileNames)
            {
                if (!File.Exists(fullFileName))
                {
                    result = fullFileName;
                    break;
                }
            }

            return result;
        }

        public static IEnumerable<string> GetFileNamesToBeOpened(DTE2 dte)
        {
            var items = GetSelectedFilesToBeOpened(dte);

            var result = new List<string>();

            foreach (var item in items)
            {
                result.Add(item);
            }

            return result;
        }

        public static IEnumerable<string> GetSelectedFilesToBeOpened(DTE2 dte)
        {
            var selectedItems = (Array)dte.ToolWindows.SolutionExplorer.SelectedItems;

            return from selectedItem in selectedItems.Cast<UIHierarchyItem>()
                   let projectItem = selectedItem.Object as ProjectItem
                   select projectItem.FileNames[1];
        }

        public static void PromptForActualExeFile(string originalPathToFile)
        {
            var box = MessageBox.Show(
               ConstantsCommon.PromptForActualExeFile(originalPathToFile),
               Vsix.Name,
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            switch (box)
            {
                case DialogResult.Yes:
                    var resultAndNamePicked = BrowseToFileLocation();
                    if (resultAndNamePicked.DialogResult == DialogResult.OK)
                    {
                        OptionsHelper.PersistVSToolOptions(resultAndNamePicked.FileNameChosen);
                    }
                    break;
                case DialogResult.No:
                    OptionsHelper.PersistVSToolOptions(originalPathToFile);
                    break;
                default:
                    break;
            }
        }

        private static FileBrowseOutcomeDto BrowseToFileLocation()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".exe",
                FileName = ConstantsSpecific.XxxFileToBrowseFor,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                CheckFileExists = true
            };

            var dialogResult = dialog.ShowDialog();

            return new FileBrowseOutcomeDto
            {
                FileNameChosen = dialog.FileName,
                DialogResult = dialogResult
            };
        }

        public static string GetDefaultTypicalFileExtensionsAsCsv(IEnumerable<string> defaultExts)
        {
            var stringBuilder = new StringBuilder();
            foreach (var defaultExt in defaultExts)
            {
                stringBuilder.Append(defaultExt).Append(',');
            }
            return stringBuilder.ToString().TrimEnd(',');
        }

        public static IEnumerable<string> GetTypicalFileExtensionAsList(string typicalFileExtensionsAsCsv)
        {
            return typicalFileExtensionsAsCsv.Split(',');
        }
    }
}

