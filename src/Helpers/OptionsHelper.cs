namespace OpenInXxx.Helpers
{
    public static class OptionsHelper
    {
        internal static void PersistVSToolOptions(string fileName)
        {
            VSPackage.Options.ActualPathToExe = fileName;
            VSPackage.Options.SaveSettingsToStorage();
        }
    }
}

