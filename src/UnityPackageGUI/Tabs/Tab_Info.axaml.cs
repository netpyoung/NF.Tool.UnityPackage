using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UnityPackageGUI;

public partial class Tab_Info : UserControl
{
    public Tab_Info()
    {
        InitializeComponent();
    }

    private void OnLinkClicked(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
        var url = "https://github.com/netpyoung/NF.Tool.UnityPackage";
        var psi = new System.Diagnostics.ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        };
        System.Diagnostics.Process.Start(psi);
    }
}