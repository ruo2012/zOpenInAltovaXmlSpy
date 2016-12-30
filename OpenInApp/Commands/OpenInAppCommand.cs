using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using OpenInApp.Common.Helpers;
using OpenInApp.Helpers;
using System;
using System.ComponentModel.Design;
using System.Linq;

namespace OpenInApp
{
    internal sealed class OpenInAppCommand 
    {
        public static OpenInAppCommand Instance { get; private set; }
        public static string Caption = Vsix.Name + " " + Vsix.Version;
        public const int CommandId = 0x0100;//gregt PackageIds.OpenInApp
        public static readonly Guid CommandSet = new Guid(PackageGuids.guidOpenInVsCmdSetString);
        
        private readonly Package _package;
        private IServiceProvider ServiceProvider { get { return this._package; } }

        public static void Initialize(Package package)
        {
            Instance = new OpenInAppCommand(package);
        }

        private OpenInAppCommand(Package package)
        {
            if (package == null)
            {
                CommonLogHelper.Log(new ArgumentNullException("package"));
                OpenInAppHelper.ShowUnexpectedError(Caption);
            }
            else
            {
                _package = package;
                var commandService = ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

                if (commandService != null)
                {
                    var menuCommandID = new CommandID(CommandSet, CommandId);
                    var menuItem = new MenuCommand(MenuItemCallback, menuCommandID);
                    commandService.AddCommand(menuItem);
                }
            }
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            var dte = (DTE2)ServiceProvider.GetService(typeof(DTE));

            try
            {
                var actualPathToExeExists = CommonFileHelper.DoesFileExist(VSPackage.Options.ActualPathToExe);

                bool proceedToExecute = true;
                if (!actualPathToExeExists)
                {
                    proceedToExecute = false;
                    FileHelper.PromptForActualExeFile(VSPackage.Options.ActualPathToExe);
                    var newActualPathToExeExists = CommonFileHelper.DoesFileExist(VSPackage.Options.ActualPathToExe);
                    if (newActualPathToExeExists)
                    {
                        proceedToExecute = true;
                    }
                    else
                    {
                        // User somehow managed to browse/select a new location for the exe that doesn't actually exist - virtually impossible, but you never know...
                        OpenInAppHelper.InformUserMissingFile(Caption, VSPackage.Options.ActualPathToExe);
                    }
                }
                if (proceedToExecute)
                {
                    var actualFilesToBeOpened = CommonFileHelper.GetFileNamesToBeOpened(dte);
                    var actualFilesToBeOpenedExist = CommonFileHelper.DoFilesExist(actualFilesToBeOpened);
                    if (!actualFilesToBeOpenedExist)
                    {
                        var missingFileName = CommonFileHelper.GetMissingFileName(actualFilesToBeOpened);
                        OpenInAppHelper.InformUserMissingFile(Caption, missingFileName);
                    }
                    else
                    {
                        var fileQuantityWarningLimitInt = VSPackage.Options.FileQuantityWarningLimitInt;
                        proceedToExecute = false;
                        if (actualFilesToBeOpened.Count() > fileQuantityWarningLimitInt)
                        {
                            proceedToExecute = OpenInAppHelper.ConfirmProceedToExecute(Caption, CommonConstants.ConfirmOpenFileQuantityExceedsWarningLimit);
                        }
                        else
                        {
                            proceedToExecute = true;
                        }
                        if (proceedToExecute)
                        {
                            var typicalFileExtensionAsList = CommonFileHelper.GetTypicalFileExtensionAsList(VSPackage.Options.TypicalFileExtensions);
                            var areTypicalFileExtensions = CommonFileHelper.AreTypicalFileExtensions(actualFilesToBeOpened, typicalFileExtensionAsList);
                            if (!areTypicalFileExtensions)
                            {
                                if (VSPackage.Options.SuppressTypicalFileExtensionsWarning)
                                {
                                    proceedToExecute = true;
                                }
                                else
                                {
                                    proceedToExecute = OpenInAppHelper.ConfirmProceedToExecute(Caption, CommonConstants.ConfirmOpenNonTypicalFile);
                                }
                            }
                            if (proceedToExecute)
                            {
                                OpenInAppHelper.InvokeCommand(actualFilesToBeOpened, VSPackage.Options.ActualPathToExe);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CommonLogHelper.Log(ex);
                OpenInAppHelper.ShowUnexpectedError(Caption);
            }
        }
    }
}