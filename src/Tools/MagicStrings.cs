using System;
using System.Collections.Generic;

namespace OpenInXxx.Tools
{
    public static class MagicStrings
    {
        //SPECIFIC
        public const string CategoryTopLevel = "Open in Altova XmlSpy";
        public const string Xxx = "XmlSpy";
        public const string XxxFolderName = "XMLSpy";
        public const string XxxGitHubRepoName = "AutoFindReplace";//TODOgregt chg to open in xml spy
        public const string XxxParentFolderName = "Altova";
        public const string XxxTypicalFileContentDescriptor = "XML";
        public static IEnumerable<string> GetDefaultTypicalFileExtensions()
        {
            //The list below largely based upon Altova's own list of supported files.
            //http://manual.altova.com/XMLSpy/spyprofessional/index.html?filetypes.htm
            return new List<string>
            {
                #region Extensions
                "asp",
                "biz",
                "cml",
                "config",
                "csproj",
                "css",
                "dcd",
                "docm",
                "docx",
                "dotm",
                "dotx",
                "dtd",
                "ent",
                "epub",
                "fo",
                "htm",
                "html",
                "json",
                "jsp",
                "math",
                "mml",
                "potm",
                "potx",
                "ppam",
                "ppsm",
                "ppsx",
                "pptm",
                "pptx",
                "properties",
                "pxf",
                "rdf",
                "runsettings",
                "settings",
                "sldm",
                "sldx",
                "sln",
                "smil",
                "sps",
                "svg",
                "testsettings",
                "thmx",
                "tld",
                "txt",
                "vml",
                "vsct",
                "vsix",
                "vsixmanifest",
                "vxml",
                "wml",
                "wsdl",
                "xbrl",
                "xdr",
                "xhtml",
                "xlam",
                "xlsb",
                "xlsm",
                "xlsx",
                "xltm",
                "xltx",
                "xml",
                "xq",
                "xql",
                "xqu",
                "xquery",
                "xsd",
                "xsig",
                "xsl",
                "xslt",
                "zip", 
	            #endregion
            };
        }

        //GENERIC
        public const string ActualPathToExeOptionDetailedDescription = "Specify the absolute install path to " + ExeFileToBrowseFor + ".";
        public const string ActualPathToExeOptionLabel = "Path to " + ExeFileToBrowseFor;
        public const string CategorySubLevel = "General";
        public static string ConfirmOpenFileQuantityExceedsWarningLimit = @"You have selected a large quantity of files to be opened.";
        public static string ConfirmOpenNonTypicalFile = @"One or more selected file types typically do not contain " + XxxTypicalFileContentDescriptor + ".";
        public static string ContinueAnyway = "Click OK to open anyway, or CANCEL to return to Visual Studio.";
        public const string ExeFileToBrowseFor = Xxx + ".exe";
        public static string FileQuantityWarningLimitInvalid = "Invalid integer value specified for:" + Environment.NewLine + Environment.NewLine + FileQuantityWarningLimitOptionLabel;
        public const string FileQuantityWarningLimitOptionDetailedDescription = "The number of files that can be opened at one time before a warning is displayed. You will be able to open files whose count exceeds this number, but you will be informed that the number of files is very high. This allows you to avoid accidentely opening hundreds or thousands of files which may affect performance of your machine.";
        public const string FileQuantityWarningLimitOptionLabel = "Simultaneous file opening count warning limit";
        public const string SuppressTypicalFileExtensionsWarningDetailedDescription = "By default you will see a warning when trying to open a file that typically does not contain " + XxxTypicalFileContentDescriptor + ". Setting this option to true will prevent this warning from being displayed.";
        public const string SuppressTypicalFileExtensionsWarningOptionLabel = "Suppress warning for non-typical " + XxxTypicalFileContentDescriptor + " files";
        public const string TypicalFileExtensionsOptionDetailedDescription = "A comma-separated list of file extensions that typically contain data in " + XxxTypicalFileContentDescriptor + " format. You can open files with extensions that are not in this list, but will be warned first that the file may not contain" + XxxTypicalFileContentDescriptor + ".";
        public const string TypicalFileExtensionsOptionLabel = "Typical " + XxxTypicalFileContentDescriptor + " files";

        public static string UnexpectedError = 
            "An unexpected error has occured. Please restart Visual Studio and re-try." + Environment.NewLine + Environment.NewLine +
            "If the error persists please log an issue at https://github.com/GregTrevellick/" + XxxGitHubRepoName + "/issues." + Environment.NewLine + Environment.NewLine +
            "Press OK to return to Visual Studio.";

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
