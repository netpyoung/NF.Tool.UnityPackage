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

public partial class Tab_Unpack : UserControl
{
    public static readonly StyledProperty<string> PackagePathProperty = AvaloniaProperty.Register<UserControl, string>(nameof(PackagePath));

    public string PackagePath
    {
        get => GetValue(PackagePathProperty);
        set => SetValue(PackagePathProperty, value);
    }

    public Tab_Unpack()
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
        bool isUnityPackage = f.Path.AbsolutePath.EndsWith(".unitypackage", StringComparison.OrdinalIgnoreCase);
        if (!isUnityPackage)
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

        IStorageItem x = filesOrNull.ElementAt(0);
        PackagePath = x.Path.AbsolutePath;
    }


    private async void OnSearchUnityPackage(object? sender, RoutedEventArgs e)
    {
        TopLevel? topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null)
        {
            return;
        }

        IReadOnlyList<IStorageFile> files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open .unitypackage",
            FileTypeFilter = [Common.FileType_UnityPackage],
            AllowMultiple = false
        });

        if (files.Count != 1)
        {
            return;
        }

        IStorageFile x = files[0];
        PackagePath = x.TryGetLocalPath()!;
    }

    private async void OnBtnUnpack(object? sender, RoutedEventArgs e)
    {

        TopLevel? topLevel = TopLevel.GetTopLevel(this);
        if (topLevel == null)
        {
            return;
        }

        if (!File.Exists(PackagePath))
        {
            _ = await MessageBoxManager.GetMessageBoxStandard(
                "Error",
                $"File does not exist in\n{PackagePath}",
                ButtonEnum.Ok,
                Icon.Error
                ).ShowWindowDialogAsync((Window)topLevel);
            return;
        }

        IReadOnlyList<IStorageFolder> folder = await topLevel.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            Title = "Select unpack directory"
        });

        if (folder.Count == 0)
        {
            return;
        }

        IStorageFolder f = folder[0];
        string outDirectory = f.TryGetLocalPath()!;

        Exception err = new Unpacker().Run(new Opt
        {
            InputUnityPackagePath = PackagePath,
            OutputDirectoryPath = outDirectory,
            IsUnpackMeta = false,
        });

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
            $"Unpack on\n{outDirectory}",
            ButtonEnum.Ok,
            Icon.Info
            ).ShowWindowDialogAsync((Window)topLevel);
    }

    public class Opt : IOptionUnpack
    {
        public string InputUnityPackagePath { get; set; }

        public string OutputDirectoryPath { get; set; }

        public bool IsUnpackMeta { get; set; }
    }
}