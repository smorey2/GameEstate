﻿using GameEstate.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class PakBinaryStream : PakBinary
    {
        public override Task ReadAsync(BinaryPakFile source, BinaryReader r, ReadStage stage)
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
                                files.Add(new FileMetadata
                                {
                                    Path = path,
                                });
                        return Task.CompletedTask;
                    }
                case ReadStage._Meta:
                    {
                        source.Process();

                        var data = r.ReadToEnd();
                        var lines = Encoding.ASCII.GetString(data)?.Split('\n');
                        if (lines?.Length == 0)
                            return Task.CompletedTask;
                        var state = -1;
                        var @params = source.Params;
                        var filesByPath = source.FilesByPath;
                        foreach (var line in lines)
                        {
                            var path = line.TrimEnd().Replace('\\', '/');
                            if (state == -1)
                            {
                                if (path == "Params:")
                                    state = 0;
                                else if (path == "AllCompressed")
                                    foreach (var file in source.Files)
                                        file.Compressed = 1;
                                else if (path == "Compressed:")
                                    state = 1;
                                else if (path == "Crypted:")
                                    state = 2;
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(line))
                                {
                                    state = -1;
                                    continue;
                                }
                                var files = filesByPath[line];
                                switch (state)
                                {
                                    case 0:
                                        var args = line.Split(new[] { ':' }, 2);
                                        @params[args[0]] = args[1];
                                        continue;
                                    case 1:
                                        if (files != null)
                                            files.First().Compressed = 1;
                                        continue;
                                    case 2:
                                        if (files != null)
                                            files.First().Crypted = true;
                                        continue;
                                }
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

        public override Task WriteAsync(BinaryPakFile source, BinaryWriter w, WriteStage stage)
        {
            switch (stage)
            {
                case WriteStage.File: return Task.CompletedTask;
                case WriteStage._Set:
                    {
                        var pathAsBytes = Encoding.ASCII.GetBytes($@"C:/{source.Name}/");
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
                        // meta
                        var @params = source.Params;
                        if (@params.Count > 0)
                        {
                            w.Write(Encoding.ASCII.GetBytes("Params:\n"));
                            foreach (var param in @params)
                            {
                                w.Write(Encoding.ASCII.GetBytes($"{param.Key}:{param.Value}"));
                                w.Write((byte)'\n');
                                w.Flush();
                            }
                            w.Write((byte)'\n');
                            w.Flush();
                        }
                        // compressed
                        var files = source.Files;
                        var numCompressed = files.Count(x => x.Compressed != 0);
                        if (files.Count == numCompressed)
                            w.Write(Encoding.ASCII.GetBytes("AllCompressed\n"));
                        else if (numCompressed > 0)
                        {
                            w.Write(Encoding.ASCII.GetBytes("Compressed:\n"));
                            foreach (var file in files.Where(x => x.Compressed != 0))
                            {
                                w.Write(Encoding.ASCII.GetBytes(file.Path));
                                w.Write((byte)'\n');
                                w.Flush();
                            }
                            w.Write((byte)'\n');
                            w.Flush();
                        }
                        // crypted
                        var numCrypted = files.Count(x => x.Crypted);
                        if (numCrypted > 0)
                        {
                            w.Write(Encoding.ASCII.GetBytes("Crypted:\n"));
                            foreach (var file in files.Where(x => x.Crypted))
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