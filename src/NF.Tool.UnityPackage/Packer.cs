using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Tar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
            var inputs = o.Inputs.Split(';');
            Debug.Assert(inputs.Length > 0);

            List<Regex> ignores = null;
            if (string.IsNullOrWhiteSpace(o.Ignores))
            {
                ignores = new List<Regex>();
            }
            else
            {
                ignores = o.Ignores.Split(';').Select(x => new Regex(x, RegexOptions.Compiled | RegexOptions.IgnoreCase)).ToList();
            }

            Regex trim = null;
            if (!string.IsNullOrWhiteSpace(o.Trim))
            {
                trim = new Regex($"^{o.Trim}");
            }

            using (var tempDirectory = new TempDirectory())
            {
                try
                {
                    foreach (var input in inputs)
                    {
                        if (ignores.Any(ignore => ignore.IsMatch(input)))
                        {
                            continue;
                        }

                        string absPath;
                        if (Path.IsPathRooted(input))
                        {
                            absPath = input;
                        }
                        else
                        {
                            absPath = Path.Combine(Environment.CurrentDirectory, input).Replace(Path.DirectorySeparatorChar, '/');
                        }

                        if (Directory.Exists(absPath))
                        {
                            ProcessDirectory(absPath, trim, o.Prefix, tempDirectory.TempDirectoryPath, ignores);
                        }
                        else if (File.Exists(absPath))
                        {
                            CreateAsset(absPath, trim, o.Prefix, tempDirectory.TempDirectoryPath);
                        }
                        else
                        {
                            return new Exception($"{absPath } is not file or directory.");
                        }
                    }

                    CreateTarGzip(tempDirectory.TempDirectoryPath, o.OutputPath);
                    return null;

                }
                catch (Exception ex)
                {
                    return ex;
                }
            }
        }

        private void CreateAsset(string inFileOrDirectory, Regex trim, string prefix, string tempDirectoryPath)
        {
            // a.txt
            // b/
            // b.meta
            //  c.txt
            //  c.txt.meta
            inFileOrDirectory = inFileOrDirectory.Replace(Path.DirectorySeparatorChar, '/');

            YamlDocument meta = GetOrGenerateMeta(inFileOrDirectory);
            string guid = GetGuid(meta);


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
            {
                var metaPath = Path.Combine(tempDirectoryPath, guid, "asset.meta");

                using (StreamWriter writer = new StreamWriter(metaPath))
                {
                    new YamlStream(meta).Save(writer, false);
                }

                FileInfo metaFile = new FileInfo(metaPath);

                using (FileStream metaFileStream = metaFile.Open(FileMode.Open))
                {
                    metaFileStream.SetLength(metaFile.Length - 3 - Environment.NewLine.Length);
                }
            }

            // guidDir/pathname
            var pathname = inFileOrDirectory.Substring(Environment.CurrentDirectory.Replace(Path.DirectorySeparatorChar, '/').Length).Trim('/');
            if (trim != null)
            {
                pathname = $"{prefix}{trim.Replace(pathname, "", 1)}";
            }
            else
            {
                pathname = $"{prefix}{pathname}";
            }

            File.WriteAllText(Path.Combine(tempDirectoryPath, guid, "pathname"), pathname);
        }

        private void ProcessDirectory(string inputDirectory, Regex trim, string prefix, string tempDirectoryPath, List<Regex> ignores)
        {
            foreach (var entry in Directory.EnumerateFileSystemEntries(inputDirectory, "*", SearchOption.AllDirectories))
            {
                if (Path.GetExtension(entry) == ".meta")
                {
                    continue;
                }

                if (ignores.Any(ignore => ignore.IsMatch(entry)))
                {
                    continue;
                }

                CreateAsset(entry.Replace(Path.DirectorySeparatorChar, '/'), trim, prefix, tempDirectoryPath);
            }
        }

        private YamlDocument GetOrGenerateMeta(string filename)
        {
            string metaPath = $"{filename}.meta";
            if (File.Exists(metaPath))
            {
                using (StreamReader reader = new StreamReader(metaPath))
                {
                    var yaml = new YamlStream();
                    yaml.Load(reader);
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

        string GetGuid(YamlDocument meta)
        {
            YamlMappingNode mapping = (YamlMappingNode)meta.RootNode;
            YamlScalarNode key = new YamlScalarNode("guid");
            YamlScalarNode value = (YamlScalarNode)mapping[key];
            return value.Value;
        }

        string CreateMD5(string input)
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

        internal void AddFilesRecursive(TarArchive archive, string directory)
        {
            string[] files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories);

            foreach (string filename in files)
            {
                TarEntry entry = TarEntry.CreateEntryFromFile(filename);
                if (archive.RootPath != null && Path.IsPathRooted(filename))
                {
#if NETSTANDARD2_0
                    entry.Name = GetRelativePath(archive.RootPath, filename);
#elif NETSTANDARD3
                    entry.Name = Path.GetRelativePath(archive.RootPath, filename);
#endif
                }
                entry.Name = entry.Name.Replace('\\', '/');
                archive.WriteEntry(entry, true);
            }
        }

        public string GetRelativePath(string relativeTo, string path)
        {
            var uri = new Uri(relativeTo);
            var rel = Uri.UnescapeDataString(uri.MakeRelativeUri(new Uri(path)).ToString()).Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            if (rel.Contains(Path.DirectorySeparatorChar.ToString()) == false)
            {
                rel = $".{ Path.DirectorySeparatorChar }{ rel }";
            }
            return rel;
        }

        private void CreateTarGzip(string inputDir, string outputTarGzip)
        {
            using (FileStream stream = new FileStream(outputTarGzip, FileMode.CreateNew))
            {
                using (GZipOutputStream zipStream = new GZipOutputStream(stream))
                {
                    using (TarArchive archive = TarArchive.CreateOutputTarArchive(zipStream))
                    {
                        archive.RootPath = inputDir.Replace(Path.DirectorySeparatorChar, '/');
                        //var tarEntry = TarEntry.CreateEntryFromFile(inputDir);
                        //tarEntry.Name = Path.GetFileName(inputDir);
                        //archive.WriteEntry(tarEntry, true);
                        //AddFilesRecursive(archive, inputDir);
                        AddDirectoryFilesToTar(archive, inputDir);
                    }
                }
            }
        }

        void AddDirectoryFilesToTar(TarArchive tarArchive, string sourceDirectory)
        {
            var filenames = Directory.GetFiles(sourceDirectory);
            foreach (var filename in filenames)
            {
                var tarEntry = TarEntry.CreateEntryFromFile(filename);
                tarEntry.Name = filename.Remove(0, tarArchive.RootPath.Length + 1);
                tarArchive.WriteEntry(tarEntry, true);
            }

            var directories = Directory.GetDirectories(sourceDirectory);
            foreach (var directory in directories)
            {
                AddDirectoryFilesToTar(tarArchive, directory);
            }
        }
    }
}