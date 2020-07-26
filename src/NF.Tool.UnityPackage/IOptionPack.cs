using System.Collections.Generic;

namespace NF.Tool.UnityPackage
{
    public interface IOptionPack
    {
        string Inputs { get; }

        string OutputPath { get; }

        string Prefix { get; }

        string Ignores { get; }

        string Trim { get; }
    }
}
