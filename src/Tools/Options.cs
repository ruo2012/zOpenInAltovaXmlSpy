using Microsoft.VisualStudio.Shell;
using OpenInXxx.Helpers;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace OpenInXxx.Tools
{
    public class Options : DialogPage
    {
        [Category(MagicStrings.CategorySubLevel)]
        [DisplayName(MagicStrings.ActualPathToExeOptionLabel)]
        [Description(MagicStrings.ActualPathToExeOptionDetailedDescription)]
        public string ActualPathToExe { get; set; } 

        [Category(MagicStrings.CategorySubLevel)]
        [DisplayName(MagicStrings.TypicalFileExtensionsOptionLabel)]
        [Description(MagicStrings.TypicalFileExtensionsOptionDetailedDescription)]
        public string TypicalFileExtensions { get; set; } = GetTypicalFileExtensions();

        [Category(MagicStrings.CategorySubLevel)]
        [DisplayName(MagicStrings.SuppressTypicalFileExtensionsWarningOptionLabel)]
        [Description(MagicStrings.SuppressTypicalFileExtensionsWarningDetailedDescription)]
        public bool SuppressTypicalFileExtensionsWarning { get; set; } = false;

        [Category(MagicStrings.CategorySubLevel)]
        [DisplayName(MagicStrings.FileQuantityWarningLimitOptionLabel)]
        [Description(MagicStrings.FileQuantityWarningLimitOptionDetailedDescription)]
        public string FileQuantityWarningLimit
        {
            get
            {
                if (string.IsNullOrEmpty(fileQuantityWarningLimit))
                {
                    return "10";
                }
                else
                {
                    return fileQuantityWarningLimit;
                }
            }
            set
            {
                int x;
                var isInteger = int.TryParse(value, out x);
                if (!isInteger)
                {
                    MessageBox.Show(
                        MagicStrings.FileQuantityWarningLimitInvalid,
                        Vsix.Name,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    fileQuantityWarningLimit = value;
                }
            }
        }

        private string fileQuantityWarningLimit;

        internal int FileQuantityWarningLimitInt
        {
            get
            {
                int x;
                var isInteger = int.TryParse(FileQuantityWarningLimit, out x);
                if (isInteger)
                {
                    return x;
                }
                else
                {
                    return 0;
                }
            }
        }

        public override void LoadSettingsFromStorage()
        {
            base.LoadSettingsFromStorage();

            if (string.IsNullOrEmpty(TypicalFileExtensions))
            {
                TypicalFileExtensions = GetTypicalFileExtensions();
            }

            if (string.IsNullOrEmpty(ActualPathToExe))
            {
                ActualPathToExe = GetActualPathToExe();
            }

            previousActualPathToExe = ActualPathToExe;
        }

        private static string GetTypicalFileExtensions()
        {
            return FileHelper.GetDefaultTypicalFileExtensionsAsCsv(MagicStrings.GetDefaultTypicalFileExtensions());
        }

        private string previousActualPathToExe { get; set; }

        private static string GetActualPathToExe()
        {
            var programFiles = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            var programFilesFolders = programFiles.Parent.GetDirectories(programFiles.Name.Replace(" (x86)", string.Empty) + "*");

            foreach (DirectoryInfo programFilesFolder in programFilesFolders)
            {
                var xxxParentFolderPaths = programFilesFolder.GetDirectories(MagicStrings.XxxParentFolderName);
                foreach (DirectoryInfo xxxParentFolderPath in xxxParentFolderPaths)
                {
                    var xxxFolderPaths = xxxParentFolderPath.GetDirectories(MagicStrings.XxxFolderName + "*");
                    foreach (DirectoryInfo xxxFolderPath in xxxFolderPaths)
                    {
                        var path = Path.Combine(xxxFolderPath.FullName, MagicStrings.ExeFileToBrowseFor);
                        if (File.Exists(path))
                        {
                            return path;
                        }
                    }
                }
            }

            return null;
        }

        protected override void OnApply(PageApplyEventArgs e)
        {
            var actualPathToExeChanged = false;

            if (ActualPathToExe != previousActualPathToExe)
            {
                actualPathToExeChanged = true;
                previousActualPathToExe = ActualPathToExe; 
            }

            if (actualPathToExeChanged)
            {
                if (!FileHelper.DoesFileExist(ActualPathToExe))
                {
                    e.ApplyBehavior = ApplyKind.Cancel;
                    FileHelper.PromptForActualExeFile(ActualPathToExe);
                }
            }

            base.OnApply(e);
        }
    }
}