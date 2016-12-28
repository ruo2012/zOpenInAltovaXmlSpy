using System;
using System.Collections.Generic;

namespace OpenInXxx.Helpers
{
    public static class ConstantsSpecific
    {
        public const string CategoryTopLevel = "Open in Altova XmlSpy";
        public const string XxxFileToBrowseFor = "XmlSpy.exe";
        public const string XxxFolderName = "XMLSpy";
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
        public const string ActualPathToExeOptionLabel = "Application path to " + XxxFileToBrowseFor;
    }
}
