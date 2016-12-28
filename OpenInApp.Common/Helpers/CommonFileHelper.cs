//using EnvDTE;
//using EnvDTE80;
//using OpenInApp.Common.Dtos;
//using OpenInApp.Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
//using System.Threading.Tasks;

namespace OpenInApp.Common.Helpers
{
    public class CommonFileHelper
    {
        public static bool AreTypicalFileExtensions(IEnumerable<string> fullFileNames, IEnumerable<string> typicalFileExtensions)
        {
            var result = false;

            var fileTypeExtensions = GetFileTypeExtensions(fullFileNames);

            foreach (var fileTypeExtension in fileTypeExtensions)
            {
                if (!string.IsNullOrEmpty(fileTypeExtension))
                {
                    result = typicalFileExtensions.Contains(fileTypeExtension.TrimStart('.'), StringComparer.CurrentCultureIgnoreCase);
                }
            }

            return result;
        }

        internal static IEnumerable<string> GetFileTypeExtensions(IEnumerable<string> fullFileNames)
        {
            var result = new List<string>();

            foreach (var fullFileName in fullFileNames)
            {
                result.Add(Path.GetExtension(fullFileName));
            }

            return result;
        }

        public static string GetMissingFileName(IEnumerable<string> fullFileNames)
        {
            var result = string.Empty;

            foreach (var fullFileName in fullFileNames)
            {
                if (!File.Exists(fullFileName))
                {
                    result = fullFileName;
                    break;
                }
            }

            return result;
        }

        public static string GetDefaultTypicalFileExtensionsAsCsv(IEnumerable<string> defaultExts)
        {
            var stringBuilder = new StringBuilder();
            foreach (var defaultExt in defaultExts)
            {
                stringBuilder.Append(defaultExt).Append(',');
            }
            return stringBuilder.ToString().TrimEnd(',');
        }

        public static IEnumerable<string> GetTypicalFileExtensionAsList(string typicalFileExtensionsAsCsv)
        {
            return typicalFileExtensionsAsCsv.Split(',');
        }

        public static bool DoesFileExist(string fullFileName)
        {
            return DoFilesExist(new List<string> { fullFileName });
        }

        public static bool DoFilesExist(IEnumerable<string> fullFileNames)
        {
            var result = true;

            foreach (var fullFileName in fullFileNames)
            {
                if (!string.IsNullOrEmpty(fullFileName))
                {
                    if (!File.Exists(fullFileName))
                    {
                        result = false;
                    }
                }
            }

            return result;
        }
    }
}
