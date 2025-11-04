using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using System;
using System.Collections.Generic;
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
        Console.WriteLine(x);
    }
}