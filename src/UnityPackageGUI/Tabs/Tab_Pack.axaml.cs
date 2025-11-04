using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
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
        if (!Directory.Exists(f.Path.AbsolutePath))
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
        PackageRootDir = x.Path.AbsolutePath;
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

        IStorageFolder x = folders[0];
        Console.WriteLine(x);

        PackageRootDir = x.Path.AbsolutePath;
    }

    private async void SaveFileButton_Clicked(object sender, RoutedEventArgs args)
    {
        // Get top level from the current control. Alternatively, you can use Window reference instead.
        var topLevel = TopLevel.GetTopLevel(this);

        // Start async operation to open the dialog.
        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = "Save Text File"
        });

        if (file is not null)
        {
            // Open writing stream from the file.
            await using var stream = await file.OpenWriteAsync();
            using var streamWriter = new StreamWriter(stream);
            // Write some content to the file.
            await streamWriter.WriteLineAsync("Hello World!");
        }
    }
}