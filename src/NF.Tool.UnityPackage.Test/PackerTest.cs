using NF.Tool.UnityPackage.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using Xunit;
using Xunit.Abstractions;

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
                IEnumerable<string> systemEntries = Directory.EnumerateFileSystemEntries(inputDir, "*", SearchOption.AllDirectories);
                string outputUnityPackage = Path.Combine(temp.TempDirectoryPath, "a.unitypackage");
                Program.OptionPack opt = new Program.OptionPack
                {
                    InputDir = inputDir,
                    OutputPath = outputUnityPackage,
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
                        string unpackedPath = Path.Combine(temp2.TempDirectoryPath, "Assets", "sample", relative);

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
    }
}