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
               Vsix.Name,
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Question);

            switch (box)
            {
                case DialogResult.Yes:
                    var resultAndNamePicked = CommonFileHelper.BrowseToFileLocation(ConstantsSpecific.ExecutableFileToBrowseFor);
                    if (resultAndNamePicked.DialogResult == DialogResult.OK)
                    {
                        PersistVSToolOptions(resultAndNamePicked.FileNameChosen);
                    }
                    break;
                case DialogResult.No:
                    PersistVSToolOptions(originalPathToFile);
                    break;
                default:
                    break;
            }
        }

        //this method cannot be pushed down w/o lots of DI refactoriung, and even then may not be possible
        private static void PersistVSToolOptions(string fileName)
        {
            VSPackage.Options.ActualPathToExe = fileName;
            VSPackage.Options.SaveSettingsToStorage();
        }
    }
}