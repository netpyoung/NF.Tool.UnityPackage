using NF.Tool.UnityPackage;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace UnityPackageGUI_Winform
{
    public partial class UnityPackageGUI_Winform_Form : Form
    {
        internal sealed class Opt_Unpack : IOptionUnpack
        {
            public string InputUnityPackagePath { get; set; }

            public string OutputDirectoryPath { get; set; }

            public bool IsUnpackMeta { get; set; }
        }

        internal sealed class Opt_Pack : IOptionPack
        {
            public required string InputDir { get; set; }
            public string OutputPath { get; set; } = string.Empty;
            public string Prefix { get; set; } = string.Empty;
        }

        const string VERSION = "v0.0.4";

        public UnityPackageGUI_Winform_Form()
        {
            InitializeComponent();

            tab_Unpack.AllowDrop = true;
            tab_Unpack.DragEnter += OnDragEnter_Unpack;
            tab_Unpack.DragDrop += OnDragDrop_Unpack;

            tab_Pack.AllowDrop = true;
            tab_Pack.DragEnter += OnDragEnter_Pack;
            tab_Pack.DragDrop += OnDragDrop_Pack;

            lbl_info.Text = @$"NF.Tool.UnityPackage {VERSION}

https://github.com/netpyoung/NF.Tool.UnityPackage/
";
        }

        #region Unpack
        private static bool IsUnityPackagePath(string f)
        {
            if (!File.Exists(f))
            {
                return false;
            }

            bool isUnityPackage = f.EndsWith(".unitypackage", StringComparison.OrdinalIgnoreCase);
            if (!isUnityPackage)
            {
                return false;
            }

            return true;
        }

        private void OnDragEnter_Unpack(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string f = files[0];
            if (!IsUnityPackagePath(f))
            {
                return;
            }

            e.Effect = DragDropEffects.Copy;
        }

        private void OnDragDrop_Unpack(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            lbl_unpack.Text = files[0];
        }

        private void btn_unitypackage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Unity Package|*.unitypackage";
                dlg.Title = "Select .unitypackage";
                dlg.ShowDialog();
                string fname = dlg.FileName;
                lbl_unpack.Text = fname;
            }
        }

        private void btn_unpack_Click(object sender, EventArgs e)
        {
            string packagePath = lbl_unpack.Text;
            if (!File.Exists(packagePath))
            {
                MessageBox.Show($"File does not exist in\n{packagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string outDirectory = string.Empty;
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result != DialogResult.OK)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return;
                }

                if (!Directory.Exists(fbd.SelectedPath))
                {
                    return;
                }

                outDirectory = fbd.SelectedPath;
            }


            Exception err = new Unpacker().Run(new Opt_Unpack
            {
                InputUnityPackagePath = packagePath,
                OutputDirectoryPath = outDirectory,
                IsUnpackMeta = check_is_unpack_meta.Checked,
            });

            if (err != null)
            {
                MessageBox.Show($"err\n{err}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Unpack\n{outDirectory}", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion // Unpack

        #region Pack
        private void OnDragEnter_Pack(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;

            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string dir = files[0];
            if (!Directory.Exists(dir))
            {
                return;
            }

            e.Effect = DragDropEffects.Copy;
        }

        private void OnDragDrop_Pack(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;

            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string dir = files[0];
            if (!Directory.Exists(dir))
            {
                return;
            }

            lbl_pack.Text = dir;
        }

        private void btn_folder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result != DialogResult.OK)
                {
                    return;
                }

                if (string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    return;
                }

                string[] files = Directory.GetFiles(fbd.SelectedPath);
                string dir = files[0];
                if (!Directory.Exists(dir))
                {
                    return;
                }

                lbl_pack.Text = dir;
            }
        }

        private void btn_pack_Click(object sender, EventArgs e)
        {
            string PackageRootDir = lbl_pack.Text;

            if (!Directory.Exists(PackageRootDir))
            {
                MessageBox.Show($"Directory does not exist in\n{PackageRootDir}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string outputUnityPackage = string.Empty;
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                dlg.Filter = "unitypackage|.unitypackage";
                dlg.ShowDialog();
                outputUnityPackage = dlg.FileName;
            }


            Packer packer = new Packer();
            Exception err = packer.Run(new Opt_Pack
            {
                InputDir = PackageRootDir,
                OutputPath = outputUnityPackage,
            });

            if (err != null)
            {
                MessageBox.Show($"err\n{err}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Pack on\n{outputUnityPackage}", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion // Pack

        private void richTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = e.LinkText,
                UseShellExecute = true
            });
        }
    }
}
