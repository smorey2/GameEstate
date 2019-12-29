using GameEstate.Core;
using System;
using System.IO;
using System.Threading.Tasks;
using Zstd.Net;

namespace GameEstate.Formats.Binary
{
    public class DatFormatRed : DatFormat
    {
        public override Task<byte[]> ReadAsync(CorePakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            byte[] buf;
            r.Position(file.Position);
            if (source.Version == PakFormatRed.BIFF_VERSION)
                buf = r.ReadBytes((int)file.FileSize);
            else if (source.Version == PakFormatRed.DZIP_VERSION)
            {
                var offsetAdd = r.ReadInt32();
                buf = new byte[file.PackedSize - offsetAdd];
                r.Skip(offsetAdd - 4);
            }
            else throw new ArgumentOutOfRangeException(nameof(source.Version), $"{source.Version}");
            ////
            //if (file.Compressed)
            //    using (var decompressor = new Decompressor())
            //        try
            //        {
            //            var src = new ArraySegment<byte>(buf);
            //            var decompressedSize = Decompressor.GetDecompressedSize(src);
            //            if (decompressedSize == 0)
            //            {
            //                source.AddRawFile(file, "Unable to Decompress");
            //                return Task.FromResult(buf);
            //            }
            //            return Task.FromResult(decompressor.Unwrap(src));
            //        }
            //        catch (ZStdException e)
            //        {
            //            exception?.Invoke(file, $"ZstdException: {e.Message}");
            //            return null;
            //        }
            return Task.FromResult(buf);
        }

        public override Task WriteAsync(CorePakFile source, BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception = null)
        {
            var buf = data;
            //if (file.Compressed)
            //    using (var compressor = new Compressor())
            //        try
            //        {
            //            var src = new ArraySegment<byte>(buf);
            //            buf = compressor.Wrap(src);
            //        }
            //        catch (ZStdException e)
            //        {
            //            exception?.Invoke(file, $"ZstdException: {e.Message}");
            //            return Task.CompletedTask;
            //        }
            w.Write(buf, 0, buf.Length);
            return Task.CompletedTask;
        }
    }
}