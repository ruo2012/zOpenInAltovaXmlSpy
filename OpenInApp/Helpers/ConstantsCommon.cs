﻿using System;
using System.Collections.Generic;

namespace OpenInApp.Helpers
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




        public const string ActualPathToExeOptionDetailedDescription = "Specify the absolute install path for the application.";
        public const string ConfirmOpenNonTypicalFile = "One or more selected files typically contains data that the application does not expect.";
        public const string SuppressTypicalFileExtensionsWarningDetailedDescription = "By default you will see a warning when trying to open a file with an extension that is not typically associated with the application. Setting this option to true will prevent this warning from being displayed.";
        public const string SuppressTypicalFileExtensionsWarningOptionLabel = "Suppress warning for non-typical file extensions";
        public const string TypicalFileExtensionsOptionDetailedDescription = "A comma-separated list of file extensions that can typically be opened in the application. You can open files with extensions that are not in this list, but will be warned first.";
        public const string TypicalFileExtensionsOptionLabel = "Typical file extensions";
        public static string UnexpectedError =
            "An unexpected error has occured. Please restart Visual Studio and re-try." + Environment.NewLine + Environment.NewLine +
            "If the error persists please log a bug for this extension via the Visual Studio Marketplace at https://marketplace.visualstudio.com" + Environment.NewLine + Environment.NewLine +
            "Press OK to return to Visual Studio.";
    }
}
