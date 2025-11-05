using CommandLine;

namespace NF.Tool.UnityPackage.Console
{
    public class Program
    {
        [Verb("unpack", HelpText = "unpack .unitypackage")]
        public class OptionUnpack : IOptionUnpack
        {
            [Option('i', "input", Required = true, HelpText = "input .unitypackage")]
            public string InputUnityPackagePath { get; set; }

            [Option('o', "output", Required = false, HelpText = "output directory")]
            public string OutputDirectoryPath { get; set; } = string.Empty;

            [Option('m', "meta", Required = false, HelpText = "unpack with meta")]
            public bool IsUnpackMeta { get; set; } = false;
        }

        [Verb("pack", HelpText = "pack.unitypackage")]
        public class OptionPack : IOptionPack
        {
            [Option('i', "input", Required = true, Separator = ';', HelpText = "input directory")]
            public string InputDir { get; set; }

            [Option('o', "output", Required = false, HelpText = "output .unitypackage")]
            public string OutputPath { get; set; } = string.Empty;
        }

        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<OptionPack, OptionUnpack>(args)
                .MapResult(
                (OptionPack o) =>
                {
                    System.Exception err = new Packer().Run(o);
                    if (err != null)
                    {
                        System.Console.Error.WriteLine(err);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                },
                (OptionUnpack o) =>
                {
                    System.Exception err = new Unpacker().Run(o);
                    if (err != null)
                    {
                        System.Console.Error.WriteLine(err);
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                },
                errs =>
                {
                    foreach (Error err in errs)
                    {
                        System.Console.Error.WriteLine(err);
                    }
                    return 1;
                });
        }
    }
}
