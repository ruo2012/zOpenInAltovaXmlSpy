using Microsoft.VisualStudio.Shell;
using OpenInApp.Common.Helpers;
using OpenInApp.Helpers;
using System.ComponentModel;
using System.Windows.Forms;

namespace OpenInApp.Options
{
    public class GeneralOptions : DialogPage
    {
        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(ConstantsSpecific.CommonActualPathToExeOptionLabel)]
        [Description(CommonConstants.ActualPathToExeOptionDetailedDescription)]
        public string ActualPathToExe { get; set; }

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.TypicalFileExtensionsOptionLabel)]
        [Description(CommonConstants.TypicalFileExtensionsOptionDetailedDescription)]
        public string TypicalFileExtensions { get; set; } = GetTypicalFileExtensions();

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.SuppressTypicalFileExtensionsWarningOptionLabel)]
        [Description(CommonConstants.SuppressTypicalFileExtensionsWarningDetailedDescription)]
        public bool SuppressTypicalFileExtensionsWarning { get; set; } = false;

        [Category(CommonConstants.CategorySubLevel)]
        [DisplayName(CommonConstants.FileQuantityWarningLimitOptionLabel)]
        [Description(CommonConstants.FileQuantityWarningLimitOptionDetailedDescription)]
        public string FileQuantityWarningLimit
        {
            get
            {
                if (string.IsNullOrEmpty(fileQuantityWarningLimit))
                {
                    return "10";//gregt make this a ConstantsCommon.cs
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
                        CommonConstants.FileQuantityWarningLimitInvalid,
                        OpenInApp.Caption,
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
                ActualPathToExe = GeneralOptionsHelper.GetActualPathToExe(
                    ConstantsSpecific.AppParentFolderName, 
                    ConstantsSpecific.AppFolderName,
                    ConstantsSpecific.ExecutableFileToBrowseFor);
            }

            previousActualPathToExe = ActualPathToExe;
        }

        private static string GetTypicalFileExtensions()
        {
            return CommonFileHelper.GetDefaultTypicalFileExtensionsAsCsv(ConstantsSpecific.GetDefaultTypicalFileExtensions());
        }

        private string previousActualPathToExe { get; set; }

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
                if (!CommonFileHelper.DoesFileExist(ActualPathToExe))
                {
                    e.ApplyBehavior = ApplyKind.Cancel;
                    FileHelper.PromptForActualExeFile(ActualPathToExe);
                }
            }

            base.OnApply(e);
        }
    }
}