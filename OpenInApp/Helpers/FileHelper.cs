using EnvDTE;
using EnvDTE80;
using OpenInApp.Common.Dtos;
using OpenInApp.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace OpenInApp.Helpers
{
    public static class FileHelper
    {


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

        public static FileBrowseOutcomeDto BrowseToFileLocation()
        {
            var dialog = new OpenFileDialog
            {
                DefaultExt = ".exe",
                FileName = ConstantsSpecific.ExecutableFileToBrowseFor,
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
    }
}

