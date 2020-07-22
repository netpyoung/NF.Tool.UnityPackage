using CommandLine;
using System;

namespace NF.Tool.UnityPackage
{
    class Program
    {
        [Verb("unpack", HelpText = "unpack .unitypackage")]
        public class OptionUnpack
        {
            [Option('i', "input", Required = true, HelpText = "input .unitypackage")]
            public string InputUnityPackagePath { get; set; }

            [Option('o', "output", Required = false, HelpText = "output directory")]
            public string OutputDirectoryPath { get; set; } = string.Empty;

            [Option('m', "meta", Required = false, HelpText = "unpack with meta")]
            public bool IsUnpackMeta { get; set; } = false;
        }

        [Verb("pack", HelpText = "pack.unitypackage")]
        public class OptionPack
        {
            [Option('i', "input", Required = true, HelpText = "input .unitypackage")]
            public string InputUnityPackagePath { get; set; }

            [Option('o', "output", Required = false, HelpText = "output directory")]
            public string OutputDirectoryPath { get; set; } = string.Empty;
        }

        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<OptionPack, OptionUnpack>(args)
                .MapResult(
                (OptionPack o) => new Packer().Run(o),
                (OptionUnpack o) => new Unpacker().Run(o),
                errs =>
                {
                    foreach (var err in errs)
                    {
                        Console.Error.WriteLine(err);
                    }
                    return 1;
                });
        }
    }
}
