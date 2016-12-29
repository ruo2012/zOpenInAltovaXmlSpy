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
        public static void ShowUnexpectedError(string vsixName, string vsixVersion)
        {
            MessageBox.Show(
                ConstantsCommon.UnexpectedError,
                vsixName + " " + vsixVersion,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
