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
    }
}