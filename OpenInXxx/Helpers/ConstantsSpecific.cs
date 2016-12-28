using System;
using System.Collections.Generic;

namespace OpenInXxx.Helpers
{
    public static class ConstantsSpecific
    {
        public const string CategoryTopLevel = "Open in Altova XmlSpy";
        public const string Xxx = "XmlSpy";
        public const string XxxFolderName = "XMLSpy";
        public const string XxxGitHubRepoName = "OpenInAltovaXmlSpy";
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

        //COMMON
        public const string ActualPathToExeOptionDetailedDescription = "Specify the absolute install path to " + ExeFileToBrowseFor + ".";
        public const string ActualPathToExeOptionLabel = "Path to " + ExeFileToBrowseFor;
        public static string GetConfirmOpenNonTypicalFile(string xxxTypicalFileContentDescriptor)
        {
            return @"One or more selected file types typically do not contain " + xxxTypicalFileContentDescriptor + ".";
        }
        public const string ExeFileToBrowseFor = Xxx + ".exe";
        public const string SuppressTypicalFileExtensionsWarningDetailedDescription = "By default you will see a warning when trying to open a file that typically does not contain " + XxxTypicalFileContentDescriptor + ". Setting this option to true will prevent this warning from being displayed.";
        public const string SuppressTypicalFileExtensionsWarningOptionLabel = "Suppress warning for non-typical " + XxxTypicalFileContentDescriptor + " files";
        public const string TypicalFileExtensionsOptionDetailedDescription = "A comma-separated list of file extensions that typically contain data in " + XxxTypicalFileContentDescriptor + " format. You can open files with extensions that are not in this list, but will be warned first that the file may not contain" + XxxTypicalFileContentDescriptor + ".";
        public const string TypicalFileExtensionsOptionLabel = "Typical " + XxxTypicalFileContentDescriptor + " files";
        public static string GetUnexpectedError(string xxxGitHubRepoName)
        {
            return
            "An unexpected error has occured. Please restart Visual Studio and re-try." + Environment.NewLine + Environment.NewLine +
            "If the error persists please log an issue at https://github.com/GregTrevellick/" + xxxGitHubRepoName + "/issues." + Environment.NewLine + Environment.NewLine +
            "Press OK to return to Visual Studio.";

        }
    }
}
