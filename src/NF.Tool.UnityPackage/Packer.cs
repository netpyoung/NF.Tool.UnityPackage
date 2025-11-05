using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using YamlDotNet.RepresentationModel;

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
            string pathname = inFileOrDirectory.Substring(baseDir.Length).Trim('/');
            pathname = $"Assets/{new DirectoryInfo(baseDir).Name}/{pathname}";
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

        private static string ToYamlString(YamlDocument document)
        {
            var stream = new YamlStream(document);
            using var writer = new StringWriter();
            stream.Save(writer, assignAnchors: false);
            return writer.ToString();
        }

        private static string ExtractGuid(string text)
        {
            var match = Regex.Match(text, @"guid:\s*([0-9a-fA-F]{32})");
            if (!match.Success)
            {
                return null;
            }

            return match.Groups[1].Value;
        }

        private void GetOrGenerateMetaStrAndGui(string inFileOrDirectory, out string outMetaStr, out string outGuid)
        {
            YamlDocument meta = GetOrGenerateMeta(inFileOrDirectory);
            if (meta != null)
            {
                outMetaStr = ToYamlString(meta);
                outMetaStr = outMetaStr.Substring(0, outMetaStr.Length - 3 - Environment.NewLine.Length);
                outGuid = GetGuid(meta);
                return;
            }
            string metaPath = $"{inFileOrDirectory}.meta";
            outMetaStr = File.ReadAllText(metaPath);
            outGuid = ExtractGuid(outMetaStr);
        }

        private YamlDocument GetOrGenerateMeta(string filename)
        {
            string metaPath = $"{filename}.meta";
            if (File.Exists(metaPath))
            {
                using (StreamReader reader = new StreamReader(metaPath))
                {
                    YamlStream yaml = new YamlStream();
                    try
                    {
                        yaml.Load(reader);
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                    return yaml.Documents[0];
                }
            }
            else
            {
                string guid = CreateMD5(filename);

                if (Directory.Exists(filename))
                {
                    return new YamlDocument(new YamlMappingNode
                        {
                            {"guid", guid},
                            {"fileFormatVersion", "2"},
                            {"folderAsset", "yes"}
                        });
                }
                else
                {
                    return new YamlDocument(new YamlMappingNode
                        {
                            {"guid", guid},
                            {"fileFormatVersion", "2"}
                        });
                }
            }
        }

        private string GetGuid(YamlDocument meta)
        {
            YamlMappingNode mapping = (YamlMappingNode)meta.RootNode;
            YamlScalarNode key = new YamlScalarNode("guid");
            YamlScalarNode value = (YamlScalarNode)mapping[key];
            return value.Value;
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
            using (FileStream stream = new FileStream(outputTarGzip, FileMode.CreateNew))
            using (GZipOutputStream zipStream = new GZipOutputStream(stream))
            using (TarArchive archive = TarArchive.CreateOutputTarArchive(zipStream))
            {
                archive.RootPath = inputDir.Replace(Path.DirectorySeparatorChar, '/');
                AddDirectoryFilesToTar(archive, inputDir);
            }
        }

        private void AddDirectoryFilesToTar(TarArchive tarArchive, string sourceDirectory)
        {
            string[] filenames = Directory.GetFiles(sourceDirectory);

            foreach (string filename in filenames)
            {
                string filenameReplaced = filename.Replace('\\', '/');
                TarEntry tarEntry = TarEntry.CreateEntryFromFile(filenameReplaced);

                string root = Path.GetPathRoot(sourceDirectory);
                tarEntry.Name = filename.Remove(0, root.Length + tarArchive.RootPath.Length + 1).Replace('\\', '/');
                tarArchive.WriteEntry(tarEntry, true);
            }

            string[] directories = Directory.GetDirectories(sourceDirectory);
            foreach (string directory in directories)
            {
                AddDirectoryFilesToTar(tarArchive, directory);
            }
        }
    }
}