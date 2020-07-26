using System;
using System.IO;
using System.Threading;

namespace NF.Tool.UnityPackage
{
    public class TempDirectory : IDisposable
    {
        static int mUID;
        public string TempDirectoryPath { get; }
        public TempDirectory(string prefix = "temp_")
        {
            if (!string.IsNullOrWhiteSpace(prefix))
            {
                TempDirectoryPath = Path.Combine(Path.GetTempPath(), prefix, Interlocked.Increment(ref mUID).ToString());
            }
            else
            {
                TempDirectoryPath = Path.Combine(Path.GetTempPath(), Interlocked.Increment(ref mUID).ToString());
            }

            Directory.CreateDirectory(TempDirectoryPath);
        }

        public void Dispose()
        {
            Directory.Delete(TempDirectoryPath, true);
        }
    }
}