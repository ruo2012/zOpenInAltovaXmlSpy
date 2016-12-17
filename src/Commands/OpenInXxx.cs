using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using OpenInXxx.Helpers;
using OpenInXxx.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OpenInXxx
{
    internal sealed class OpenInXxx
    {
        public static OpenInXxx Instance { get; private set; }
        public const int CommandId = 0x0100;
        public static readonly Guid CommandSet = new Guid("82afac2b-5d6f-43f7-8c37-c575653bc07c");

        private readonly Package _package;
        private IServiceProvider ServiceProvider { get { return this._package; } }

        public static void Initialize(Package package)
        {
            Instance = new OpenInXxx(package);
        }

        private OpenInXxx(Package package)
        {
            if (package == null)
            {
                Logger.Log(new ArgumentNullException("package"));
                ShowUnexpectedError();
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
                var actualPathToExeExists = FileHelper.DoesFileExist(VSPackage.Options.ActualPathToExe);

                bool proceedToExecute = true;
                if (!actualPathToExeExists)
                {
                    proceedToExecute = false;
                    FileHelper.PromptForActualExeFile(VSPackage.Options.ActualPathToExe);
                    var newActualPathToExeExists = FileHelper.DoesFileExist(VSPackage.Options.ActualPathToExe);
                    if (newActualPathToExeExists)
                    {
                        proceedToExecute = true;
                    }
                    else
                    {
                        // User somehow managed to browse/select a new location for the exe that doesn't actually exist - virtually impossible, but you never know...
                        InformUserMissingFile(VSPackage.Options.ActualPathToExe);
                    }
                }
                if (proceedToExecute)
                {
                    var actualFilesToBeOpened = FileHelper.GetFileNamesToBeOpened(dte);
                    var actualFilesToBeOpenedExist = FileHelper.DoFilesExist(actualFilesToBeOpened);
                    if (!actualFilesToBeOpenedExist)
                    {
                        var missingFileName = FileHelper.GetMissingFileName(actualFilesToBeOpened);
                        InformUserMissingFile(missingFileName);
                    }
                    else
                    {
                        var fileQuantityWarningLimitInt = VSPackage.Options.FileQuantityWarningLimitInt;
                        proceedToExecute = false;
                        if (actualFilesToBeOpened.Count() > fileQuantityWarningLimitInt)
                        {
                            proceedToExecute = ConfirmProceedToExecute(MagicStrings.ConfirmOpenFileQuantityExceedsWarningLimit);
                        }
                        else
                        {
                            proceedToExecute = true;
                        }
                        if (proceedToExecute)
                        {
                            var typicalFileExtensionAsList = FileHelper.GetTypicalFileExtensionAsList(VSPackage.Options.TypicalFileExtensions);
                            var areTypicalFileExtensions = FileHelper.AreTypicalFileExtensions(actualFilesToBeOpened, typicalFileExtensionAsList);
                            if (!areTypicalFileExtensions)
                            {
                                if (VSPackage.Options.SuppressTypicalFileExtensionsWarning)
                                {
                                    proceedToExecute = true;
                                }
                                else
                                {
                                    proceedToExecute = ConfirmProceedToExecute(MagicStrings.ConfirmOpenNonTypicalFile);
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
                Logger.Log(ex);
                ShowUnexpectedError();
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

        private static bool ConfirmProceedToExecute(string messageText)
        {
            bool proceedToExecute = false;

            messageText += Environment.NewLine + Environment.NewLine + MagicStrings.ContinueAnyway;

            var box = MessageBox.Show(
                messageText,
                Vsix.Name,
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning);

            if (box == DialogResult.OK)
            {
                proceedToExecute = true;
            }

            return proceedToExecute;
        }

        private static void InformUserMissingFile(string missingFileName)
        {
            MessageBox.Show(
                MagicStrings.InformUserMissingFile(missingFileName),
                Vsix.Name,
                MessageBoxButtons.OK,
                MessageBoxIcon.Stop);
        }

        private static void ShowUnexpectedError()
        {
            MessageBox.Show(
                MagicStrings.UnexpectedError,
                Vsix.Name,
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}