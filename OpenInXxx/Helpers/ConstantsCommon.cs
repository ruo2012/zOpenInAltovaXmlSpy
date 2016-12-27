using System;
using System.Collections.Generic;

namespace OpenInXxx.Helpers
{
    public static class ConstantsCommon
    {

        public const string CategorySubLevel = "General";
        public static string ConfirmOpenFileQuantityExceedsWarningLimit = @"You have selected a large quantity of files to be opened.";
   
        public static string ContinueAnyway = "Click OK to open anyway, or CANCEL to return to Visual Studio.";

        public static string FileQuantityWarningLimitInvalid = "Invalid integer value specified for:" + Environment.NewLine + Environment.NewLine + FileQuantityWarningLimitOptionLabel;
        public const string FileQuantityWarningLimitOptionDetailedDescription = "The number of files that can be opened at one time before a warning is displayed. You will be able to open files whose count exceeds this number, but you will be informed that the number of files is very high. This allows you to avoid accidentely opening hundreds or thousands of files which may affect performance of your machine.";
        public const string FileQuantityWarningLimitOptionLabel = "Simultaneous file opening count warning limit";
      

        public static string InformUserMissingFile(string missingFileName)
        {
            return $"The file \"{missingFileName}\" does not exist.";
        }

        public static string PromptForActualExeFile(string dodgyPathToFile)
        {
            return InformUserMissingFile(dodgyPathToFile)
                + Environment.NewLine + Environment.NewLine
                + "Do you want to browse the for the file ?"
                + Environment.NewLine + Environment.NewLine
                + "Click YES to locate the file, NO to save anyway.";
        }
    }
}
