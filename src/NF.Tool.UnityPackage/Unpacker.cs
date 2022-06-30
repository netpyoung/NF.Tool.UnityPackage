using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace NF.Tool.UnityPackage
{
    public class Unpacker
    {
        public Unpacker()
        {
        }

        public Exception Run(IOptionUnpack o)
        {
            // .unitypackage
            //   - guidDir/
            //   | - asset
            //   | - asset.meta
            //   | - pathname
            using (TempDirectory tempDirectory = new TempDirectory())
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(o.OutputDirectoryPath))
                    {
                        Directory.CreateDirectory(o.OutputDirectoryPath);
                    }

                    ExtractTarGzip(o.InputUnityPackagePath, tempDirectory.TempDirectoryPath);
                    foreach (string guidDir in Directory.GetDirectories(tempDirectory.TempDirectoryPath))
                    {
                        string pathnamePath = Path.Combine(guidDir, "pathname");
                        if (!File.Exists(pathnamePath))
                        {
                            continue;
                        }

                        string pathname = File.ReadLines(pathnamePath).First().TrimEnd();
                        string assetOutPath = Path.Combine(o.OutputDirectoryPath, pathname);
                        string assetOutDirPath = Path.GetDirectoryName(assetOutPath);
                        if (!Directory.Exists(assetOutDirPath))
                        {
                            Directory.CreateDirectory(assetOutDirPath);
                        }

                        string assetPath = Path.Combine(guidDir, "asset");
                        if (File.Exists(assetPath))
                        {
                            File.Move(assetPath, assetOutPath);
                        }

                        string assetMetaPath = Path.ChangeExtension(assetPath, "meta");
                        if (o.IsUnpackMeta && File.Exists(assetMetaPath))
                        {
                            File.Move(assetMetaPath, $"{assetOutPath}.meta");
                        }
                    }
                    return null;
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }

        }

        private void ExtractTarGzip(string gzArchiveName, string destFolder)
        {
            using (FileStream inStream = File.OpenRead(gzArchiveName))
            using (GZipInputStream gzipStream = new GZipInputStream(inStream))
            using (TarArchive tarArchive = TarArchive.CreateInputTarArchive(gzipStream, Encoding.UTF8))
            {
                tarArchive.ExtractContents(destFolder);
            }
        }
    }
}