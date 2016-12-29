using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using OpenInApp.Common.Helpers;
using OpenInApp.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace OpenInApp
{
    internal sealed class OpenInApp
    {
        public static OpenInApp Instance { get; private set; }
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("82afac2b-5d6f-43f7-8c37-c575653bc07c");

        private readonly Package _package;
        private IServiceProvider ServiceProvider { get { return this._package; } }

        public static void Initialize(Package package)
        {
            Instance = new OpenInApp(package);
        }

        private OpenInApp(Package package)
        {
            if (package == null)
            {
                LogHelper.Log(new ArgumentNullException("package"));
                OpenInAppHelper.ShowUnexpectedError(Vsix.Name, Vsix.Version);
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
                        OpenInAppHelper.InformUserMissingFile(Vsix.Name, Vsix.Version, VSPackage.Options.ActualPathToExe);
                    }
                }
                if (proceedToExecute)
                {
                    var actualFilesToBeOpened = CommonFileHelper.GetFileNamesToBeOpened(dte);
                    var actualFilesToBeOpenedExist = CommonFileHelper.DoFilesExist(actualFilesToBeOpened);
                    if (!actualFilesToBeOpenedExist)
                    {
                        var missingFileName = CommonFileHelper.GetMissingFileName(actualFilesToBeOpened);
                        OpenInAppHelper.InformUserMissingFile(Vsix.Name, Vsix.Version, missingFileName);
                    }
                    else
                    {
                        var fileQuantityWarningLimitInt = VSPackage.Options.FileQuantityWarningLimitInt;
                        proceedToExecute = false;
                        if (actualFilesToBeOpened.Count() > fileQuantityWarningLimitInt)
                        {
                            proceedToExecute = OpenInAppHelper.ConfirmProceedToExecute(Vsix.Name, Vsix.Version, CommonConstants.ConfirmOpenFileQuantityExceedsWarningLimit);
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
                                    proceedToExecute = OpenInAppHelper.ConfirmProceedToExecute(Vsix.Name, Vsix.Version, CommonConstants.ConfirmOpenNonTypicalFile);
                                }
                            }
                            if (proceedToExecute)
                            {
                                InvokeCommand(actualFilesToBeOpened);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Log(ex);
                OpenInAppHelper.ShowUnexpectedError(Vsix.Name, Vsix.Version);
            }
        }

        private static void InvokeCommand(IEnumerable<string> actualFilesToBeOpened)
        {
            var fullPath = VSPackage.Options.ActualPathToExe;
            var arguments = " ";

            foreach (var actualFileToBeOpened in actualFilesToBeOpened)
            {
                arguments += "\"" + actualFileToBeOpened + "\"" + " ";
            }

            var start = new ProcessStartInfo()
            {
                WorkingDirectory = Path.GetDirectoryName(fullPath),
                FileName = Path.GetFileName(fullPath),
                Arguments = arguments,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            try
            {
                using (System.Diagnostics.Process.Start(start)) { }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}