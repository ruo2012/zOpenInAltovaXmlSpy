using Microsoft.VisualStudio.Shell;
using OpenInXxx.Helpers;
using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace OpenInXxx.Options
{
    public class GeneralOptions : DialogPage
    {
        [Category(ConstantsCommon.CategorySubLevel)]
        [DisplayName(ConstantsSpecific.ActualPathToExeOptionLabel)]
        [Description(ConstantsSpecific.ActualPathToExeOptionDetailedDescription)]
        public string ActualPathToExe { get; set; } 

        [Category(ConstantsCommon.CategorySubLevel)]
        [DisplayName(ConstantsSpecific.TypicalFileExtensionsOptionLabel)]
        [Description(ConstantsSpecific.TypicalFileExtensionsOptionDetailedDescription)]
        public string TypicalFileExtensions { get; set; } = GetTypicalFileExtensions();

        [Category(ConstantsCommon.CategorySubLevel)]
        [DisplayName(ConstantsSpecific.SuppressTypicalFileExtensionsWarningOptionLabel)]
        [Description(ConstantsSpecific.SuppressTypicalFileExtensionsWarningDetailedDescription)]
        public bool SuppressTypicalFileExtensionsWarning { get; set; } = false;

        [Category(ConstantsCommon.CategorySubLevel)]
        [DisplayName(ConstantsCommon.FileQuantityWarningLimitOptionLabel)]
        [Description(ConstantsCommon.FileQuantityWarningLimitOptionDetailedDescription)]
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
                        ConstantsCommon.FileQuantityWarningLimitInvalid,
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
            return FileHelper.GetDefaultTypicalFileExtensionsAsCsv(ConstantsSpecific.GetDefaultTypicalFileExtensions());
        }

        private string previousActualPathToExe { get; set; }

        private static string GetActualPathToExe()
        {
            var programFiles = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
            var programFilesFolders = programFiles.Parent.GetDirectories(programFiles.Name.Replace(" (x86)", string.Empty) + "*");

            foreach (DirectoryInfo programFilesFolder in programFilesFolders)
            {
                var xxxParentFolderPaths = programFilesFolder.GetDirectories(ConstantsSpecific.XxxParentFolderName);
                foreach (DirectoryInfo xxxParentFolderPath in xxxParentFolderPaths)
                {
                    var xxxFolderPaths = xxxParentFolderPath.GetDirectories(ConstantsSpecific.XxxFolderName + "*");
                    foreach (DirectoryInfo xxxFolderPath in xxxFolderPaths)
                    {
                        var path = Path.Combine(xxxFolderPath.FullName, ConstantsSpecific.ExeFileToBrowseFor);
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