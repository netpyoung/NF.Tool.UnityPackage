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
            [Option('i', "inputs", Required = true, Separator = ';', HelpText = "input paths (seperate by ';')")]
            public string Inputs { get; set; }

            [Option('o', "output", Required = false, HelpText = "output .unitypackage")]
            public string OutputPath { get; set; } = string.Empty;

            [Option('p', "prefix", Required = false, HelpText = "add output prefix")]
            public string Prefix { get; set; } = string.Empty;

            [Option('r', "ignores", Required = false, Separator = ';', HelpText = "ignore regexs (seperate by ';')")]
            public string Ignores { get; set; } = string.Empty;

            [Option('t', "trim", Required = false, HelpText = "trim output prefix")]
            public string Trim { get; set; } = string.Empty;
        }

        static int Main(string[] args)
        {
            return Parser.Default.ParseArguments<OptionPack, OptionUnpack>(args)
                .MapResult(
                (OptionPack o) =>
                {
                    var err = new Packer().Run(o);
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
                    var err = new Unpacker().Run(o);
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
                    foreach (var err in errs)
                    {
                        System.Console.Error.WriteLine(err);
                    }
                    return 1;
                });
        }
    }
}
