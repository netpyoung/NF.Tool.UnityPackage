namespace NF.Tool.UnityPackage
{
    public interface IOptionUnpack
    {
        string InputUnityPackagePath { get; }

        string OutputDirectoryPath { get; }

        bool IsUnpackMeta { get; }
    }
}
