using System;
using System.IO;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace NF.Tool.UnityPackage.Test
{
    public class UnitTest1
    {
        ITestOutputHelper output;
        public UnitTest1(ITestOutputHelper output)
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
                int result = new Unpacker().Run(new Program.OptionUnpack
                {
                    InputUnityPackagePath = packagePath,
                    OutputDirectoryPath = temp.TempDirectoryPath,
                    IsUnpackMeta = false,
                });
                Assert.Equal(0, result);
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
                int result = new Unpacker().Run(new Program.OptionUnpack
                {
                    InputUnityPackagePath = packagePath,
                    OutputDirectoryPath = temp.TempDirectoryPath,
                    IsUnpackMeta = false,
                });
                Assert.Equal(0, result);
                Assert.True(Directory.Exists(temp.TempDirectoryPath));
                Assert.True(Directory.Exists(Path.Combine(temp.TempDirectoryPath, "Assets")));
                Assert.True(File.Exists(Path.Combine(temp.TempDirectoryPath, "Assets", "test.txt")));
                Assert.Equal("testing", File.ReadAllText(Path.Combine(temp.TempDirectoryPath, "Assets", "test.txt")));
            }
        }
    }
}
