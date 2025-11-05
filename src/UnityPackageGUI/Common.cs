using Avalonia.Platform.Storage;

namespace UnityPackageGUI
{
    internal static class Common
    {
        public static FilePickerFileType FileType_UnityPackage { get; } = new FilePickerFileType("unitypackage")
        {
            Patterns = ["*.unitypackage"],
        };

        public static FolderPickerOpenOptions OpenOption_Package { get; } = new FolderPickerOpenOptions
        {
            Title = "Root Folder",
            AllowMultiple = false
        };
    }
}
