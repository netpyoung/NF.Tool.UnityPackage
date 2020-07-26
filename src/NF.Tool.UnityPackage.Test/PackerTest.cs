using NF.Tool.UnityPackage.Console;
using System;
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
            using (var temp = new TempDirectory())
            {
                var inputDir = "../../../sample";
                var prefix = "sample2";
                var systemEntries = Directory.EnumerateFileSystemEntries(inputDir, "*", SearchOption.AllDirectories);
                var outputUnityPackage = Path.Combine(temp.TempDirectoryPath, "a.unitypackage");
                var opt = new Program.OptionPack
                {
                    Inputs = inputDir,
                    OutputPath = outputUnityPackage,
                    Prefix = prefix,
                    Trim = "../../../sample",
                };
                var packer = new Packer();
                var err = packer.Run(opt);

                Assert.Null(err);

                Assert.True(File.Exists(outputUnityPackage));


                using (var temp2 = new TempDirectory())
                {
                    var err2 = new Unpacker().Run(new Program.OptionUnpack
                    {
                        InputUnityPackagePath = outputUnityPackage,
                        OutputDirectoryPath = temp2.TempDirectoryPath,
                        IsUnpackMeta = true
                    });
                    Assert.Null(err2);
                    foreach (var entry in systemEntries)
                    {
                        var relative = Path.GetRelativePath(inputDir, entry);
                        var unpackedPath = Path.Combine(temp2.TempDirectoryPath, prefix, relative);

                        if (Directory.Exists(entry))
                        {
                            Assert.True(Directory.Exists(unpackedPath));
                        }
                        else
                        {
                            Assert.True(File.Exists(unpackedPath));
                            var a = File.ReadAllBytes(entry);
                            var b = File.ReadAllBytes(unpackedPath);
                            Assert.Equal(a, b);
                        }
                    }
                }
            }
        }

        private static byte[] GetMD5(string file)
        {
            using (MD5 md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(file))
                {
                    return md5.ComputeHash(stream);
                }
            }
        }
    }
}