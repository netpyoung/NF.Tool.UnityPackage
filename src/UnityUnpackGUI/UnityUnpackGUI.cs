using NF.Tool.UnityPackage;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace UnityUnpackGUI
{
    public partial class UnityUnpackGUI : Form
    {
        public UnityUnpackGUI()
        {
            InitializeComponent();
        }

        public class Opt : IOptionUnpack
        {
            public string InputUnityPackagePath { get; set; }

            public string OutputDirectoryPath { get; set; }

            public bool IsUnpackMeta { get; set; }
        }

        private void OnTxDrag_DragDrop(object sender, DragEventArgs e)
        {
            string[] unityPackagePaths = GetSrcUnityPackagePathsOrNull(e);
            if (unityPackagePaths == null)
            {
                return;
            }

            foreach (string unityPackagePath in unityPackagePaths)
            {
                string filename = Path.GetFileNameWithoutExtension(unityPackagePath);
                string dir = Path.GetDirectoryName(unityPackagePath);
                string outDir = Path.Combine(dir, filename);
                Opt o = new Opt
                {
                    InputUnityPackagePath = unityPackagePath,
                    IsUnpackMeta = false,
                    OutputDirectoryPath = outDir,
                };
                System.Exception err = new Unpacker().Run(o);
                if (err != null)
                {
                    MessageBox.Show("EE", "aa", MessageBoxButtons.OK);
                }
            }
        }
        private string[] GetSrcUnityPackagePathsOrNull(DragEventArgs e)
        {
            bool isFileDrop = e.Data.GetDataPresent(DataFormats.FileDrop);
            if (!isFileDrop)
            {
                return null;
            }

            object dropData = e.Data.GetData(DataFormats.FileDrop);
            if (dropData == null)
            {
                return null;
            }

            string[] dropPaths = dropData as string[];
            return dropPaths;
        }

        private void OnTxtDrag_DragEnter(object sender, DragEventArgs e)
        {
            string[] unityPackagePaths = GetSrcUnityPackagePathsOrNull(e);
            if (unityPackagePaths == null)
            {
                return;
            }
            foreach (string unityPackagePath in unityPackagePaths)
            {
                if (Path.GetExtension(unityPackagePath) != ".unitypackage")
                {
                    return;
                }
            }
            e.Effect = DragDropEffects.Link;
        }
    }
}