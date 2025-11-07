using System;
using System.Formats.Tar;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace NF.Tool.UnityPackage
{
    public class Packer
    {
        public Packer()
        {
        }

        public Exception Run(IOptionPack o)
        {
            string input = o.InputDir;
            string absPath = Path.GetFullPath(input).Replace('\\', '/');

            if (!Directory.Exists(absPath))
            {
                return new Exception($"{absPath} is not file or directory.");
            }

            using (TempDirectory tempDirectory = new TempDirectory())
            {
                try
                {
                    ProcessDirectory(absPath, tempDirectory.TempDirectoryPath);
                    CreateTarGzip(tempDirectory.TempDirectoryPath, o.OutputPath);
                    return null;
                }
                catch (Exception ex)
                {
                    return ex;
                }
            }
        }

        private void CreateAsset(string baseDir, string inFileOrDirectory, string tempDirectoryPath)
        {
            // a.txt
            // b/
            // b.meta
            //  c.txt
            //  c.txt.meta
            inFileOrDirectory = inFileOrDirectory.Replace(Path.DirectorySeparatorChar, '/');

            GetOrGenerateMetaStrAndGui(inFileOrDirectory, out string metaStr, out string guid);

            // .unitypackage
            //   - guidDir/
            //   | - asset
            //   | - asset.meta
            //   | - pathname

            // guidDir/
            Directory.CreateDirectory(Path.Combine(tempDirectoryPath, guid));

            // guidDir/asset
            if (File.Exists(inFileOrDirectory))
            {
                File.Copy(inFileOrDirectory, Path.Combine(tempDirectoryPath, guid, "asset"));
            }

            // guidDir/asset.meta
            string metaPath = Path.Combine(tempDirectoryPath, guid, "asset.meta");
            File.WriteAllText(metaPath, metaStr);

            // guidDir/pathname
            string dir = string.Empty;
            if (IsOnUnityProject(inFileOrDirectory, out string outP))
            {
                dir = inFileOrDirectory.Substring(outP.Length).Trim('/');
            }
            else
            {
                dir = $"Assets/{new DirectoryInfo(baseDir).Name}";
            }
            string pathname = inFileOrDirectory.Substring(baseDir.Length).Trim('/');
            pathname = $"{dir}/{pathname}";
            File.WriteAllText(Path.Combine(tempDirectoryPath, guid, "pathname"), pathname);
        }

        private void ProcessDirectory(string inputDirectory, string tempDirectoryPath)
        {
            foreach (string entry in Directory.EnumerateFileSystemEntries(inputDirectory, "*", SearchOption.AllDirectories))
            {
                if (Path.GetExtension(entry) == ".meta")
                {
                    continue;
                }

                CreateAsset(inputDirectory, entry.Replace('\\', '/'), tempDirectoryPath);
            }
        }

        private static string ExtractGuid(string text)
        {
            Match match = Regex.Match(text, @"guid:\s*([0-9a-fA-F]{32})");
            if (!match.Success)
            {
                return null;
            }

            return match.Groups[1].Value;
        }

        private void GetOrGenerateMetaStrAndGui(string inFileOrDirectory, out string outMetaStr, out string outGuid)
        {
            string metaPath = $"{inFileOrDirectory}.meta";
            if (File.Exists(metaPath))
            {
                outMetaStr = File.ReadAllText(metaPath);
                outGuid = ExtractGuid(outMetaStr);
            }

            string guid = CreateMD5(inFileOrDirectory);
            if (Directory.Exists(inFileOrDirectory))
            {
                outMetaStr = string.Join('\n', ["fileFormatVersion: 2", $"guid: {guid}", "folderAsset: yes"]);
                outGuid = guid;
            }
            else
            {
                outMetaStr = string.Join('\n', ["fileFormatVersion: 2", $"guid: {guid}", ""]);
                outGuid = guid;
            }
        }

        private string CreateMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();

                foreach (byte b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        private void CreateTarGzip(string inputDir, string outputTarGzip)
        {
            using FileStream stream = new FileStream(outputTarGzip, FileMode.CreateNew);
            using (GZipStream gzipStream = new GZipStream(stream, CompressionMode.Compress))
            {
                TarFile.CreateFromDirectory(inputDir, gzipStream, includeBaseDirectory: false);
            }
        }

        private bool IsOnUnityProject(string fullpath, out string outP)
        {
            if (!fullpath.Contains("Assets", StringComparison.OrdinalIgnoreCase))
            {
                outP = string.Empty;
                return false;
            }

            DirectoryInfo dir = new DirectoryInfo(fullpath);

            while (dir != null)
            {
                if (!Directory.Exists(Path.Combine(dir.FullName, "Assets")))
                {
                    dir = dir.Parent;
                    continue;
                }

                string manifestPath = Path.Combine(dir.FullName, "Packages", "manifest.json");
                if (File.Exists(manifestPath))
                {
                    outP = dir.FullName.Replace('\\', '/');
                    return true;
                }

                string versionPath = Path.Combine(dir.FullName, "ProjectSettings", "ProjectVersion.txt");
                if (File.Exists(versionPath))
                {
                    outP = dir.FullName.Replace('\\', '/');
                    return true;
                }

                dir = dir.Parent;
            }


            outP = string.Empty;
            return false;
        }
    }
}