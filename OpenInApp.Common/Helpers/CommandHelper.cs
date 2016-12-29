using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenInApp.Common.Helpers
{
    public class CommandHelper
    {
        public static bool ConfirmProceedToExecute(string vsixName, string vsixVersion, string messageText)
        {
            bool proceedToExecute = false;

            messageText += Environment.NewLine + Environment.NewLine + ConstantsCommon.ContinueAnyway;

            var box = MessageBox.Show(
                messageText,
                vsixName + " " + vsixVersion,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (box == DialogResult.OK)
            {
                proceedToExecute = true;
            }

            return proceedToExecute;
        }

        public static void ShowUnexpectedError(string vsixName, string vsixVersion)
        {
            MessageBox.Show(
                ConstantsCommon.UnexpectedError,
                vsixName + " " + vsixVersion,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        public static void InformUserMissingFile(string vsixName, string vsixVersion, string missingFileName)
        {
            MessageBox.Show(
                ConstantsCommon.InformUserMissingFile(missingFileName),
                vsixName + " " + vsixVersion,
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);
        }
    }
}
