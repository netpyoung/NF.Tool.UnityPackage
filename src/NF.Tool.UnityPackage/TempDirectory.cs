using System;
using System.IO;

namespace NF.Tool.UnityPackage
{
    public class TempDirectory : IDisposable
    {
        public string TempDirectoryPath { get; }
        public TempDirectory()
        {
            TempDirectoryPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(TempDirectoryPath);
        }

        public void Dispose()
        {
            Directory.Delete(TempDirectoryPath, true);
        }
    }
}