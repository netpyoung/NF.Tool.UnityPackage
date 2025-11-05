using Avalonia.Controls;
using System.Diagnostics;

namespace UnityPackageGUI;

public partial class Tab_Info : UserControl
{
    public Tab_Info()
    {
        InitializeComponent();
    }

    private void OnLinkClicked(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        string url = "https://github.com/netpyoung/NF.Tool.UnityPackage";
        ProcessStartInfo psi = new()
        {
            FileName = url,
            UseShellExecute = true
        };
        Process.Start(psi);
    }
}