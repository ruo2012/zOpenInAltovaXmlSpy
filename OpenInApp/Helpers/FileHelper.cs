using OpenInApp.Commands;
using OpenInApp.Common.Helpers;
using System.Windows.Forms;

namespace OpenInApp.Helpers
{
    public static class FileHelper
    {
        public static void PromptForActualExeFile(string originalPathToFile)
        {
            var box = MessageBox.Show(
               CommonConstants.PromptForActualExeFile(originalPathToFile),
               OpenInAppCommand.Caption,
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            switch (box)
            {
                case DialogResult.Yes:
                    var resultAndNamePicked = CommonFileHelper.BrowseToFileLocation(ConstantsForApp.ExecutableFileToBrowseFor);
                    if (resultAndNamePicked.DialogResult == DialogResult.OK)
                    {
                        PersistVSToolOptions(resultAndNamePicked.FileNameChosen);
                    }
                    break;
                case DialogResult.No:
                    PersistVSToolOptions(originalPathToFile);
                    break;
            }
        }

        private static void PersistVSToolOptions(string fileName)
        {
            VSPackage.Options.ActualPathToExe = fileName;
            VSPackage.Options.SaveSettingsToStorage();
        }
    }
}