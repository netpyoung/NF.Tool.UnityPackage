using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using NF.Tool.UnityPackage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace UnityPackageGUI;

public partial class Tab_Pack : UserControl
{
    public static readonly StyledProperty<string> PackageRootDirProperty = AvaloniaProperty.Register<UserControl, string>(nameof(PackageRootDir));

    public string PackageRootDir
    {
        get => GetValue(PackageRootDirProperty);
        set => SetValue(PackageRootDirProperty, value);
    }

    public Tab_Pack()
    {
        InitializeComponent();
        DataContext = this;
    }

    private async void OnDragOver(object? sender, DragEventArgs e)
    {
        e.DragEffects = DragDropEffects.None;

        if (!e.Data.Contains(DataFormats.Files))
        {
            return;
        }

        IEnumerable<IStorageItem>? filesOrNull = e.Data.GetFiles();
        if (filesOrNull == null)
        {
            return;
        }

        IStorageItem f = filesOrNull.ElementAt(0);
        string dir = f.TryGetLocalPath()!;
        if (!Directory.Exists(dir))
        {
            return;
        }

        e.DragEffects = DragDropEffects.Copy;
    }

    private void OnDrop(object? sender, DragEventArgs e)
    {
        IEnumerable<IStorageItem>? filesOrNull = e.Data.GetFiles();
        if (filesOrNull == null)
        {
            return;
        }

        IStorageItem f = filesOrNull.ElementAt(0);
        string dir = f.TryGetLocalPath()!;
        PackageRootDir = dir;
    }

    private async void OnSearchPackageRootFolder(object? sender, RoutedEventArgs e)
    {
        TopLevel? topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null)
        {
            return;
        }

        IReadOnlyList<IStorageFolder> folders = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Root Folder",
            AllowMultiple = false,
        });

        if (folders.Count != 1)
        {
            return;
        }

        IStorageItem f = folders.ElementAt(0);
        string dir = f.TryGetLocalPath()!;
        PackageRootDir = dir;
    }

    private async void OnBtnPack(object sender, RoutedEventArgs args)
    {
        TopLevel? topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null)
        {
            return;
        }

        if (!Directory.Exists(PackageRootDir))
        {
            _ = await MessageBoxManager.GetMessageBoxStandard(
                "Error",
                $"Directory does not exist in\n{PackageRootDir}            ",
                ButtonEnum.Ok,
                Icon.Error
                ).ShowWindowDialogAsync((Window)topLevel);
            return;
        }

        IStorageFile? file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save UnityPackage File",
            DefaultExtension = "unitypackage",
            FileTypeChoices = [new FilePickerFileType("unitypackage")
            {
                Patterns = ["*.unitypackage"],
            }],
        });

        if (file == null)
        {
            return;
        }
        string outputUnityPackage = file.TryGetLocalPath()!;

        Opt opt = new Opt
        {
            Inputs = PackageRootDir,
            OutputPath = outputUnityPackage,
            Prefix = PackageRootDir,
            Trim = string.Empty,
        };
        Packer packer = new Packer();
        Exception err = packer.Run(opt);

        if (err != null)
        {
            _ = await MessageBoxManager.GetMessageBoxStandard(
                "Error",
                $"err\n{err}",
                ButtonEnum.Ok,
                Icon.Error
                ).ShowWindowDialogAsync((Window)topLevel);
            return;
        }

        _ = await MessageBoxManager.GetMessageBoxStandard(
            "Done",
            $"Pack on\n{outputUnityPackage}            ",
            ButtonEnum.Ok,
            Icon.Info
            ).ShowWindowDialogAsync((Window)topLevel);
    }

    public class Opt : IOptionPack
    {
        public string Inputs { get; set; }
        public string OutputPath { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;
        public string Ignores { get; set; } = string.Empty;
        public string Trim { get; set; } = string.Empty;
    }
}