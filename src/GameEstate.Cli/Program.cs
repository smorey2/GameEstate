﻿using CommandLine;
using GameEstate.Core;
using GameEstate.Formats.Binary;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameEstate
{
    // list -e "Tes"
    // import -e "Tes" -u "game:/Oblivion*.bsa#Oblivion" --path "D:\T_\Test2"
    // export -e "Tes" -u "game:/Oblivion*.bsa#Oblivion" --path "D:\T_\Test2"
    // xsport -e "Tes" -u "game:/Oblivion*.bsa#Oblivion" --path "D:\T_\Test2"
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

        #region Options

        [Verb("list", HelpText = "Extract files contents to folder.")]
        class ListOptions
        {
            [Option('e', "estate", HelpText = "Estate")]
            public string Estate { get; set; }

            [Option('u', "uri", HelpText = "Pak file to be extracted")]
            public Uri Uri { get; set; }
        }

        [Verb("export", HelpText = "Extract files contents to folder.")]
        class ExportOptions
        {
            [Option('e', "estate", Required = true, HelpText = "Estate")]
            public string Estate { get; set; }

            [Option('u', "uri", Required = true, HelpText = "Pak file to be extracted")]
            public Uri Uri { get; set; }

            [Option("path", Default = @".\out", HelpText = "Output folder")]
            public string Path { get; set; }

            [Option("fix", Default = false, HelpText = "Fix")]
            public bool Fix { get; set; }
        }

        [Verb("import", HelpText = "Insert files contents to pak.")]
        class ImportOptions
        {
            [Option('e', "estate", Required = true, HelpText = "Estate")]
            public string Estate { get; set; }

            [Option('u', "uri", Required = true, HelpText = "Pak file to be created")]
            public Uri Uri { get; set; }

            [Option("path", Default = @".\out", HelpText = "Insert folder")]
            public string Path { get; set; }
        }

        [Verb("xsport", HelpText = "Insert files contents to pak.")]
        class XsportOptions
        {
            [Option('e', "estate", Required = true, HelpText = "Estate")]
            public string Estate { get; set; }

            [Option('u', "uri", Required = true, HelpText = "Pak file to be created")]
            public Uri Uri { get; set; }

            [Option("path", Default = @".\out", HelpText = "Insert folder")]
            public string Path { get; set; }
        }

        #endregion

        static string[] args00 = new[] { "list" };
        static string[] args01 = new[] { "list", "-e", "Red" };
        static string[] args02 = new[] { "list", "-e", "Tes", "-u", "game:/Oblivion*.bsa#Oblivion" };
        static string[] args03 = new[] { "list", "-e", "Tes", "-u", "file:///D:/T_/Oblivion/Oblivion*.bsa#Oblivion" };

        static string[] argsRsi1 = new[] { "export", "-e", "Rsi", "-u", "game:/Data.p4k#StarCitizen", "--path", @"D:\T_\StarCitizen" };

        static string[] argsTes1 = new[] { "export", "-e", "Tes", "-u", "game:/Oblivion*.bsa#Oblivion", "--path", @"D:\T_\Oblivion" };
        static string[] argsTes2 = new[] { "import", "-e", "Tes", "-u", "game:/Oblivion*.bsa#Oblivion", "--path", @"D:\T_\Oblivion" };
        //
        static string[] argsRed1 = new[] { "export", "-e", "Red", "-u", "game:/main.key#Witcher", "--path", @"D:\T_\Witcher" };
        static string[] argsRed2 = new[] { "export", "-e", "Red", "-u", "game:/krbr.dzip#Witcher2", "--path", @"D:\T_\Witcher2" };

        static void Main(string[] args) => Parser.Default.ParseArguments<ListOptions, ExportOptions, ImportOptions, XsportOptions>(argsTes1)
            .MapResult(
                  (ListOptions opts) => RunListAsync(opts).GetAwaiter().GetResult(),
                  (ExportOptions opts) => RunExportAsync(opts).GetAwaiter().GetResult(),
                  (ImportOptions opts) => RunImportAsync(opts).GetAwaiter().GetResult(),
                  (XsportOptions opts) => RunXsportAsync(opts).GetAwaiter().GetResult(),
                  errs => 1);

        static Task<int> RunListAsync(ListOptions opts)
        {
            // list estates
            if (string.IsNullOrEmpty(opts.Estate))
            {
                Console.WriteLine("Estates installed:\n");
                foreach (var estate2 in CoreEstate.Estates)
                    Console.WriteLine($"{estate2.Key} - {estate2.Value.Name}");
                return Task.FromResult(0);
            }

            var estate = CoreEstate.GetEstate(opts.Estate);
            // list found locations in estate
            if (opts.Uri == null)
            {
                var estateGames = string.Join(", ", estate.GetGames().Select(x => x.description));
                Console.WriteLine($"{estate.Name}\n{estate.Description}\nGames: {estateGames}\n");
                var locations = estate.FileManager.Locations;
                if (locations.Count == 0)
                {
                    Console.WriteLine("No games found.");
                    return Task.FromResult(0);
                }
                Console.WriteLine("Locations found:\n");
                foreach (var location in locations)
                {
                    var (name, description) = estate.GetGame(location.Key);
                    Console.WriteLine($"{description} - {location.Value}");
                }
                return Task.FromResult(0);
            }
            // list files in pack for estate
            else
            {
                Console.WriteLine($"{estate.Name} - {opts.Uri}\n");
                using (var multPak = estate.OpenPakFile(estate.ParseResource(opts.Uri)))
                {
                    if (multPak.Paks.Count == 0)
                    {
                        Console.WriteLine("No paks found.");
                        return Task.FromResult(0);
                    }
                    Console.WriteLine("Paks found:");
                    foreach (var pak in multPak.Paks)
                    {
                        Console.WriteLine($"\n{pak.Name}");
                        foreach (var exts in pak.Files.Select(x => Path.GetExtension(x.Path)).GroupBy(x => x))
                            Console.WriteLine($"  files{exts.Key}: {exts.Count()}");
                    }
                }
            }
            return Task.FromResult(0);
        }

        static async Task<int> RunExportAsync(ExportOptions opts)
        {
            var from = ProgramState.Load(data => Convert.ToInt32(data), 0);
            var estate = CoreEstate.GetEstate(opts.Estate);
            using (var multPak = estate.OpenPakFile(estate.ParseResource(opts.Uri)))
            {
                // write paks header
                var filePath = opts.Path;
                if (!string.IsNullOrEmpty(filePath) && !Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                var setPath = Path.Combine(filePath, ".set");
                using (var w = new BinaryWriter(new FileStream(setPath, FileMode.Create, FileAccess.Write)))
                    await PakFormat.Stream.WriteAsync(new StreamPakFile("Root") { Files = multPak.Paks.Select(x => new FileMetadata { Path = x.Name }).ToList() }, w, PakFormat.WriteStage._Set);

                // write paks
                foreach (var pak in multPak.Paks)
                {
                    var newPath = Path.Combine(filePath, Path.GetFileName(pak.FilePath));
                    await pak.ExportAsync(newPath, from, (file, index) =>
                    {
                        //if ((index % 50) == 0)
                        //    Console.WriteLine($"{file.Path}");
                    }, (file, message) =>
                    {
                        Console.WriteLine($"{message}: {file?.Path}");
                    });
                }
            }
            ProgramState.Clear();
            return 0;
        }

        static async Task<int> RunImportAsync(ImportOptions opts)
        {
            var from = ProgramState.Load(data => Convert.ToInt32(data), 0);
            var estate = CoreEstate.GetEstate(opts.Estate);
            foreach (var path in estate.ParseResource(opts.Uri).Paths)
            {
                using (var pak = estate.OpenPakFile(path))
                using (var w = new BinaryWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
                    await pak.ImportAsync(w, opts.Path, from, (file, index) =>
                    {
                        //if ((index % 50) == 0)
                        //    Console.WriteLine($"{file.Path}");
                    }, (file, message) =>
                    {
                        Console.WriteLine($"{message}: {file?.Path}");
                    });
            }
            ProgramState.Clear();
            return 0;
        }

        static async Task<int> RunXsportAsync(XsportOptions opts)
        {
            var from = ProgramState.Load(data => Convert.ToInt32(data), 0);
            var estate = CoreEstate.GetEstate(opts.Estate);
            foreach (var path in estate.ParseResource(opts.Uri).Paths)
            {
                using (var pak = estate.OpenPakFile(path))
                using (var w = new BinaryWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
                    await pak.ImportAsync(w, opts.Path, from, (file, index) =>
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