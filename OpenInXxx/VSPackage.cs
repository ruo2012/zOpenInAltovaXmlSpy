using Microsoft.VisualStudio.Shell;
using OpenInXxx.Options;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace OpenInXxx
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]       
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(VSPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    [ProvideOptionPage(typeof(GeneralOptions), Helpers.Constants.CategoryTopLevel, Helpers.Constants.CategorySubLevel, 0, 0, true)]
    public sealed class VSPackage : Package
    {
        public static GeneralOptions Options { get; private set; }

        public const string PackageGuidString = "62a5a896-9442-4aa3-a87c-0daece0e04a5";

        public VSPackage()
        {
        }

        protected override void Initialize()
        {
            Options = (GeneralOptions)GetDialogPage(typeof(GeneralOptions));

            OpenInXxx.Initialize(this);
            base.Initialize();
        }
    }
}
