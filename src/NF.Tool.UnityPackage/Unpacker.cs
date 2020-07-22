using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using System;
using System.IO;
using System.Linq;

namespace NF.Tool.UnityPackage
{
    internal class Unpacker
    {
        public Unpacker()
        {
        }

        public int Run(Program.OptionUnpack o)
        {
            using (var tempDirectory = new TempDirectory())
            {
                try
                {
                    Directory.CreateDirectory(o.OutputDirectoryPath);
                    ExtractTarGzip(o.InputUnityPackagePath, tempDirectory.TempDirectoryPath);
                    foreach (var dirEntry in Directory.GetDirectories(tempDirectory.TempDirectoryPath))
                    {
                        var pathnamePath = Path.Combine(dirEntry, "pathname");
                        var assetPath = Path.Combine(dirEntry, "asset");
                        if (!File.Exists(pathnamePath) || !File.Exists(assetPath))
                        {
                            continue;
                        }

                        var pathname = File.ReadLines(pathnamePath).First().TrimEnd();
                        var assetOutPath = Path.Combine(o.OutputDirectoryPath, pathname);
                        var assetOupDirPath = Path.GetDirectoryName(assetOutPath);
                        if (!Directory.Exists(assetOupDirPath))
                        {
                            Directory.CreateDirectory(assetOupDirPath);
                        }
                        File.Move(assetPath, assetOutPath);
                    }
                    return 0;
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                    return 1;
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