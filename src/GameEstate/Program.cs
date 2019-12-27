using CommandLine;
using GameEstate.Core;
using GameEstate.Rsi;
using GameEstate.Tes;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate
{
    class Program
    {
        #region ProgramState

        static class ProgramState
        {
            public static T Load<T>(Func<byte[], T> action, T defaultValue)
            {
                try
                {
                    if (File.Exists(@".\lastChunk.txt"))
                        using (var s = File.Open(@".\lastChunk.txt", FileMode.Open))
                        {
                            var data = new byte[s.Length];
                            s.Read(data, 0, (int)s.Length);
                            return action(data);
                        }
                }
                catch { }
                return defaultValue;
            }

            public static void Store(Func<byte[]> action)
            {
                try
                {
                    var data = action();
                    using (var s = new FileStream(@".\lastChunk.txt", FileMode.Create, FileAccess.Write))
                        s.Write(data, 0, data.Length);
                }
                catch { Clear(); }
            }

            public static void Clear()
            {
                try
                {
                    if (File.Exists(@".\lastChunk.txt"))
                        File.Delete(@".\lastChunk.txt");
                }
                catch { }
            }
        }

        #endregion

        [Verb("extract", HelpText = "Extract files contents to folder.")]
        class ExtractOptions
        {
            [Option('e', "estate", Required = true, HelpText = "Estate")]
            public string Estate { get; set; }

            [Option('u', "uri", Required = true, HelpText = "Pak file to be extracted")]
            public Uri Uri { get; set; }

            [Option("path", Default = @".\out", HelpText = "Output folder")]
            public string Path { get; set; }
        }

        [Verb("insert", HelpText = "Insert files contents to pak.")]
        class InsertOptions
        {
            [Option('e', "estate", Required = true, HelpText = "Estate")]
            public string Estate { get; set; }

            [Option('u', "uri", Required = true, HelpText = "Pak file to be created")]
            public Uri Uri { get; set; }

            [Option("path", Default = @".\out", HelpText = "Insert folder")]
            public string Path { get; set; }
        }

        static void Main(string[] args) => Parser.Default.ParseArguments<ExtractOptions, InsertOptions>(args)
            .MapResult(
                  (ExtractOptions opts) => RunExtractAsync(opts).GetAwaiter().GetResult(),
                  (InsertOptions opts) => RunInsertAsync(opts).GetAwaiter().GetResult(),
                  errs => 1);

        static async Task<int> RunExtractAsync(ExtractOptions opts)
        {
            var from = ProgramState.Load(data => Convert.ToInt32(data), 0);
            var estate = CoreEstate.Parse(opts.Estate);
            using (var multPak = estate.OpenPakFile(estate.ParseResource(opts.Uri)))
                foreach (var pak in multPak.Paks)
                {
                    var newPath = Path.Combine(opts.Path, Path.GetFileName(pak.FilePath));
                    await pak.ExtractAsync(newPath, from, (file, index) =>
                    {
                        //if ((index % 50) == 0)
                        //Console.WriteLine($"{file.Path}");
                    }, (file, message) =>
                    {
                        Console.WriteLine($"{message}: {file.Path}");
                    });
                }
            ProgramState.Clear();
            return 0;
        }

        static async Task<int> RunInsertAsync(InsertOptions opts)
        {
            var from = ProgramState.Load(data => Convert.ToInt32(data), 0);
            var estate = CoreEstate.Parse(opts.Estate);
            foreach (var path in estate.ParseResource(opts.Uri, false).Paths)
            {
                using (var pak = estate.OpenPakFile(path))
                using (var w = new BinaryWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
                    await pak.InsertAsync(w, opts.Path, from, (file, index) =>
                    {
                        //if ((index % 50) == 0)
                        //Console.WriteLine($"{file.Path}");
                    }, (file, message) =>
                    {
                        Console.WriteLine($"{message}: {file.Path}");
                    });
            }
            ProgramState.Clear();
            return 0;
        }
    }
}