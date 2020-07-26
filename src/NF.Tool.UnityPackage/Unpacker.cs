using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using System;
using System.IO;
using System.Linq;

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
            using (var tempDirectory = new TempDirectory())
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(o.OutputDirectoryPath))
                    {
                        Directory.CreateDirectory(o.OutputDirectoryPath);
                    }

                    ExtractTarGzip(o.InputUnityPackagePath, tempDirectory.TempDirectoryPath);
                    foreach (var guidDir in Directory.GetDirectories(tempDirectory.TempDirectoryPath))
                    {
                        var pathnamePath = Path.Combine(guidDir, "pathname");
                        if (!File.Exists(pathnamePath))
                        {
                            continue;
                        }

                        var pathname = File.ReadLines(pathnamePath).First().TrimEnd();
                        var assetOutPath = Path.Combine(o.OutputDirectoryPath, pathname);
                        var assetOutDirPath = Path.GetDirectoryName(assetOutPath);
                        if (!Directory.Exists(assetOutDirPath))
                        {
                            Directory.CreateDirectory(assetOutDirPath);
                        }

                        var assetPath = Path.Combine(guidDir, "asset");
                        if (File.Exists(assetPath))
                        {
                            File.Move(assetPath, assetOutPath);
                        }

                        var assetMetaPath = Path.ChangeExtension(assetPath, "meta");
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
            using (var inStream = File.OpenRead(gzArchiveName))
            {
                using (var gzipStream = new GZipInputStream(inStream))
                {
                    using (var tarArchive = TarArchive.CreateInputTarArchive(gzipStream))
                    {
                        tarArchive.ExtractContents(destFolder);
                    }
                }
            }
        }
    }
}