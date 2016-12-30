using OpenInApp.Common.Helpers;
using System.Collections.Generic;

namespace OpenInApp.Helpers
{
    public static class ConstantsForApp 
    {
        public const string AppFolderName = "Altova";
        public const string AppSubFolderName = "XMLSpy";//null in most cases
        public const string ExecutableFileToBrowseFor = "XmlSpy.exe";
        public static IEnumerable<string> GetDefaultTypicalFileExtensions()
        {
            return new List<string>
            {
                //See also http://manual.altova.com/XMLSpy/spyprofessional/index.html?filetypes.htm
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
                "nuspec",
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



        internal const string CommonActualPathToExeOptionLabel = CommonConstants.ActualPathToExeOptionLabelPrefix + ExecutableFileToBrowseFor;
    }
}
