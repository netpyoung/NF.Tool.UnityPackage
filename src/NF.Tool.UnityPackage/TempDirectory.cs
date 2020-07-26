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

                TempDirectoryPath = Path.Combine(Path.GetTempPath(), prefix, $"{Path.GetRandomFileName()}.{Interlocked.Increment(ref mUID)}");
            }
            else
            {
                TempDirectoryPath = Path.Combine(Path.GetTempPath(), $"{Path.GetRandomFileName()}.{Interlocked.Increment(ref mUID)}");
            }

            Directory.CreateDirectory(TempDirectoryPath);
        }

        public void Dispose()
        {
            Directory.Delete(TempDirectoryPath, true);
        }
    }
}