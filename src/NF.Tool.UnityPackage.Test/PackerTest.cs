﻿using NF.Tool.UnityPackage.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Xunit;
using Xunit.Abstractions;
using YamlDotNet.RepresentationModel;

namespace NF.Tool.UnityPackage.Test
{
    public class PackerTest
    {
        ITestOutputHelper output;
        public PackerTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestPackerPack()
        {
            using (TempDirectory temp = new TempDirectory())
            {
                string inputDir = "../../../sample";
                string prefix = "sample2";
                IEnumerable<string> systemEntries = Directory.EnumerateFileSystemEntries(inputDir, "*", SearchOption.AllDirectories);
                string outputUnityPackage = Path.Combine(temp.TempDirectoryPath, "a.unitypackage");
                Program.OptionPack opt = new Program.OptionPack
                {
                    Inputs = inputDir,
                    OutputPath = outputUnityPackage,
                    Prefix = prefix,
                    Trim = "../../../sample",
                };
                Packer packer = new Packer();
                Exception err = packer.Run(opt);

                Assert.Null(err);

                Assert.True(File.Exists(outputUnityPackage));


                using (TempDirectory temp2 = new TempDirectory())
                {
                    Exception err2 = new Unpacker().Run(new Program.OptionUnpack
                    {
                        InputUnityPackagePath = outputUnityPackage,
                        OutputDirectoryPath = temp2.TempDirectoryPath,
                        IsUnpackMeta = true
                    });
                    Assert.Null(err2);
                    foreach (string entry in systemEntries)
                    {
                        string relative = Path.GetRelativePath(inputDir, entry);
                        string unpackedPath = Path.Combine(temp2.TempDirectoryPath, prefix, relative);

                        if (Directory.Exists(entry))
                        {
                            Assert.True(Directory.Exists(unpackedPath));
                        }
                        else
                        {
                            Assert.True(File.Exists(unpackedPath));
                            if (Path.GetExtension(unpackedPath) == ".meta")
                            {
                                //var a = GetYamlDocument(entry);
                                //var b = GetYamlDocument(unpackedPath);
                                //Assert.Equal(a, b);
                            }
                            else
                            {
                                byte[] a = File.ReadAllBytes(entry);
                                byte[] b = File.ReadAllBytes(unpackedPath);
                                Assert.Equal(a, b);
                            }
                        }
                    }
                }
            }
        }

        YamlDocument GetYamlDocument(string metaPath)
        {
            using (StreamReader reader = new StreamReader(metaPath))
            {
                YamlStream yaml = new YamlStream();
                yaml.Load(reader);
                return yaml.Documents[0];
            }
        }

        private static byte[] GetMD5(string file)
        {
            using (MD5 md5 = MD5.Create())
            using (FileStream stream = File.OpenRead(file))
            {
                return md5.ComputeHash(stream);
            }
        }
    }
}