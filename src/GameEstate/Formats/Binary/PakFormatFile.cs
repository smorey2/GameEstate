using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakFormatFile : PakFormat
    {
        public override Task ReadAsync(CorePakFile source, BinaryReader r, ReadStage stage)
        {
            switch (stage)
            {
                case ReadStage.File: return Task.CompletedTask;
                case ReadStage._Set:
                    {
                        var files = source.Files = new List<FileMetadata>();
                        var data = r.ReadToEnd();
                        // dir /s/b/a-d > .set
                        var lines = Encoding.ASCII.GetString(data)?.Split('\n');
                        if (lines?.Length == 0)
                            return Task.CompletedTask;
                        string path;
                        var startIndex = Path.GetDirectoryName(lines[0].TrimEnd().Replace('\\', '/')).Length + 1;
                        foreach (var line in lines)
                            if (line.Length >= startIndex && (path = line.Substring(startIndex).TrimEnd().Replace('\\', '/')) != ".set")
                            {
                                files.Add(new FileMetadata
                                {
                                    Path = path,
                                });
                            }
                        return Task.CompletedTask;
                    }
                case ReadStage._Meta:
                    {
                        source.Process();
                        var filesByPath = source.FilesByPath;
                        var data = r.ReadToEnd();
                        var lines = Encoding.ASCII.GetString(data)?.Split('\n');
                        if (lines?.Length == 0)
                            return Task.CompletedTask;
                        var state = 0;
                        foreach (var line in lines)
                        {
                            var path = line.TrimEnd().Replace('\\', '/');
                            if (state == 0)
                            {
                                if (path == "HasNamePrefix")
                                    source.HasNamePrefix = true;
                                else if (path == "Extra")
                                    source.HasExtra = true;
                                else if (path == "Compressed")
                                    foreach (var file in source.Files)
                                        file.Compressed = true;
                                else if (path == "Compressed:")
                                    state = 1;
                            }
                            else
                                switch (state)
                                {
                                    case 1:
                                        if (string.IsNullOrEmpty(line))
                                        {
                                            state = 0;
                                            continue;
                                        }
                                        var files = filesByPath[line];
                                        if (files != null)
                                            files.First().Compressed = true;
                                        continue;
                                }
                        }
                        return Task.CompletedTask;
                    }
                case ReadStage._Raw:
                    {
                        var filesRawSet = source.FilesRawSet = new HashSet<string>();
                        var data = r.ReadToEnd();
                        var lines = Encoding.ASCII.GetString(data)?.Split('\n');
                        if (lines?.Length == 0)
                            return Task.CompletedTask;
                        foreach (var line in lines)
                            filesRawSet.Add(line.TrimEnd().Replace('\\', '/'));
                        return Task.CompletedTask;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());
            }
        }

        public override Task WriteAsync(CorePakFile source, BinaryWriter w, WriteStage stage)
        {
            switch (stage)
            {
                case WriteStage.File: return Task.CompletedTask;
                case WriteStage._Set:
                    {
                        var pathAsBytes = Encoding.ASCII.GetBytes($@"C:\{Path.GetFileNameWithoutExtension(source.FilePath)}\");
                        w.Write(pathAsBytes);
                        w.Write(Encoding.ASCII.GetBytes(".set"));
                        w.Write((byte)'\n');
                        w.Flush();
                        // files
                        var files = source.Files;
                        foreach (var file in files) //.OrderBy(x => x.Path))
                        {
                            w.Write(pathAsBytes);
                            w.Write(Encoding.ASCII.GetBytes(file.Path));
                            w.Write((byte)'\n');
                            w.Flush();
                        }
                        return Task.CompletedTask;
                    }
                case WriteStage._Meta:
                    {
                        var files = source.Files;
                        // meta
                        if (source.HasNamePrefix)
                            w.Write(Encoding.ASCII.GetBytes("HasNamePrefix\n"));
                        if (source.HasExtra)
                            w.Write(Encoding.ASCII.GetBytes("Extra\n"));
                        // compressed
                        var numCompressed = files.Count(x => x.Compressed);
                        if (files.Count == numCompressed)
                            w.Write(Encoding.ASCII.GetBytes("Compressed\n"));
                        else if (numCompressed > 0)
                        {
                            w.Write(Encoding.ASCII.GetBytes("Compressed:\n"));
                            foreach (var file in files.Where(x => x.Compressed))
                            {
                                w.Write(Encoding.ASCII.GetBytes(file.Path));
                                w.Write((byte)'\n');
                                w.Flush();
                            }
                            w.Write((byte)'\n');
                            w.Flush();
                        }
                        return Task.CompletedTask;
                    }
                case WriteStage._Raw:
                    {
                        if (source.FilesRawSet == null)
                            throw new ArgumentNullException(nameof(source.FilesRawSet));
                        foreach (var file in source.FilesRawSet)
                        {
                            w.Write(Encoding.ASCII.GetBytes(file));
                            w.Write((byte)'\n');
                            w.Flush();
                        }
                        return Task.CompletedTask;
                    }
                default: throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());
            }
        }
    }
}