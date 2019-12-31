using GameEstate.Formats.Binary;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Core
{
    public static class MultiPakExtensions
    {
        #region Export / Import

        public static async Task ExportAsync(this CorePakFile source, string filePath, int from = 0, Action<FileMetadata, int> advance = null, Action<FileMetadata, string> exception = null)
        {
            // write pak
            if (!string.IsNullOrEmpty(filePath) && !Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            // write files
            var hasExtra = source.HasExtra;
            Parallel.For(from, source.Files.Count, new ParallelOptions { /*MaxDegreeOfParallelism = 1*/ }, async index =>
            {
                var file = source.Files[index];
                var newPath = Path.Combine(filePath, file.Path);

                // create directory
                var directory = Path.GetDirectoryName(newPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                // extract pak
                if (file.Pak != null)
                    await file.Pak.ExportAsync(newPath);

                // skip empty file
                if (file.FileSize == 0 && file.PackedSize == 0)
                {
                    advance?.Invoke(file, index);
                    return;
                }

                // extract file
                try
                {
                    var b = await source.LoadFileDataAsync(file, exception);
                    using (var s = new FileStream(newPath, FileMode.Create, FileAccess.Write))
                        s.Write(b, 0, b.Length);
                    if (hasExtra && file.ExtraSize > 0)
                    {
                        b = await source.LoadExtraDataAsync(file, exception);
                        using (var s = new FileStream($"{newPath}.~", FileMode.Create, FileAccess.Write))
                            s.Write(b, 0, b.Length);
                    }
                    advance?.Invoke(file, index);
                }
                catch (Exception e) { exception?.Invoke(file, $"Exception: {e.Message}"); }
            });

            // write pak-raw
            await new StreamPakFile(source, filePath).WriteAsync(null, PakFormat.WriteStage.File);

            //// write pak-raw
            //if (source.FilesRawSet != null && source.FilesRawSet.Count > 0)
            //    using (var w = new BinaryWriter(new FileStream(rawPath, FileMode.Create, FileAccess.Write)))
            //        await PakFormat.Stream.WriteAsync(source, w, PakFormat.WriteStage._Raw);
        }

        public static async Task ImportAsync(this CorePakFile source, BinaryWriter w, string filePath, int from = 0, Action<FileMetadata, int> advance = null, Action<FileMetadata, string> exception = null)
        {
            // read pak
            if (string.IsNullOrEmpty(filePath) || !Directory.Exists(filePath))
            {
                exception?.Invoke(null, $"Directory Missing: {filePath}");
                return;
            }
            var setPath = Path.Combine(filePath, ".set");
            using (var r = new BinaryReader(File.Open(setPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                await PakFormat.Stream.ReadAsync(source, r, PakFormat.ReadStage._Set);
            var metaPath = Path.Combine(filePath, ".meta");
            using (var r = new BinaryReader(File.Open(setPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                await PakFormat.Stream.ReadAsync(source, r, PakFormat.ReadStage._Meta);
            var rawPath = Path.Combine(filePath, ".raw");
            if (File.Exists(rawPath))
                using (var r = new BinaryReader(File.Open(rawPath, FileMode.Open, FileAccess.Read, FileShare.Read)))
                    await PakFormat.Stream.ReadAsync(source, r, PakFormat.ReadStage._Raw);

            // write header
            if (from == 0)
                await source.PakFormat.WriteAsync(source, w, PakFormat.WriteStage.Header);

            // write files
            var hasExtra = source.HasExtra;
            Parallel.For(0, source.Files.Count, new ParallelOptions { MaxDegreeOfParallelism = 1 }, async index =>
            {
                var file = source.Files[index];
                var newPath = Path.Combine(filePath, file.Path);

                // check directory
                var directory = Path.GetDirectoryName(newPath);
                if (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
                {
                    exception?.Invoke(file, $"Directory Missing: {directory}");
                    return;
                }

                // insert file
                try
                {
                    await source.PakFormat.WriteAsync(source, w, PakFormat.WriteStage.File);
                    using (var r = File.Open(newPath, FileMode.Open, FileAccess.Read, FileShare.Read))
                        await source.WriteFileDataAsync(w, file, r.ReadAllBytes(), exception);
                    //if (hasExtra && file.ExtraSize > 0)
                    //{
                    //    b = await source.LoadExtraDataAsync(file, exception);
                    //    using (var s = new FileStream($"{newPath}.~", FileMode.Create, FileAccess.Write))
                    //        s.Write(b, 0, b.Length);
                    //}
                    advance?.Invoke(file, index);
                }
                catch (Exception e) { exception?.Invoke(file, $"Exception: {e.Message}"); }
            });
        }

        #endregion
    }
}