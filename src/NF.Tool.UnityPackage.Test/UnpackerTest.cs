using System;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;
using NF.Tool.UnityPackage.Console;

namespace NF.Tool.UnityPackage.Test
{
    public class UnpackerTest
    {
        ITestOutputHelper output;
        public UnpackerTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void TestPackageExtract()
        {
            using (var temp = new TempDirectory())
            {
                var packagePath = "../../../test.unitypackage";

                Assert.True(File.Exists(packagePath));
                var err = new Unpacker().Run(new Program.OptionUnpack
                {
                    InputUnityPackagePath = packagePath,
                    OutputDirectoryPath = temp.TempDirectoryPath,
                    IsUnpackMeta = false,
                });
                Assert.Null(err);
                Assert.True(Directory.Exists(temp.TempDirectoryPath));
                Assert.True(Directory.Exists(Path.Combine(temp.TempDirectoryPath, "Assets")));
                Assert.True(File.Exists(Path.Combine(temp.TempDirectoryPath, "Assets", "test.txt")));
                Assert.Equal("testing", File.ReadAllText(Path.Combine(temp.TempDirectoryPath, "Assets", "test.txt")));

            }
        }

        [Fact]
        public void TestPackageExtractWithLeadingDots()
        {
            using (var temp = new TempDirectory())
            {
                var packagePath = "../../../testLeadingDots.unitypackage";

                Assert.True(File.Exists(packagePath));
                var err = new Unpacker().Run(new Program.OptionUnpack
                {
                    InputUnityPackagePath = packagePath,
                    OutputDirectoryPath = temp.TempDirectoryPath,
                    IsUnpackMeta = false,
                });
                Assert.Null(err);
                Assert.True(Directory.Exists(temp.TempDirectoryPath));
                Assert.True(Directory.Exists(Path.Combine(temp.TempDirectoryPath, "Assets")));
                Assert.True(File.Exists(Path.Combine(temp.TempDirectoryPath, "Assets", "test.txt")));
                Assert.Equal("testing", File.ReadAllText(Path.Combine(temp.TempDirectoryPath, "Assets", "test.txt")));
            }
        }
    }
}
