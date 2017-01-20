using Microsoft.VisualStudio.Shell;
using OpenInApp.Commands;
using OpenInApp.Common.Helpers;
using OpenInApp.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace OpenInApp
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", Vsix.Version, IconResourceID = 400)]       
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageGuids.guidOpenInAppPackageString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideOptionPage(typeof(GeneralOptions), Vsix.Name, CommonConstants.CategorySubLevel, 0, 0, true)]
    public sealed class VSPackage : Package
    {
        public static GeneralOptions Options { get; private set; }

        protected override void Initialize()
        {
            Options = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));

            OpenInAppCommand.Initialize(this);
            base.Initialize();
        }
    }
}
