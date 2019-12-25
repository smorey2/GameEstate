using System;
using System.IO;
using System.Threading.Tasks;
using ZstdNet;

namespace GameEstate.Core.DataFormat
{
    public class DatFormat01 : DatFormat
    {
        public override Task<byte[]> ReadAsync(CorePakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            var buf = new byte[file.PackedSize];
            r.Position(file.Position);
            r.Read(buf, 0, buf.Length);
            if (file.Compressed)
                using (var decompressor = new Decompressor())
                    try
                    {
                        var src = new ArraySegment<byte>(buf);
                        var decompressedSize = Decompressor.GetDecompressedSize(src);
                        if (decompressedSize == 0)
                        {
                            exception?.Invoke(file, "Unable to Decompress");
                            return Task.FromResult(buf);
                        }
                        return Task.FromResult(decompressor.Unwrap(src));
                    }
                    catch (ZstdException e)
                    {
                        exception?.Invoke(file, $"ZstdException: {e.Message}");
                        return null;
                    }
            return Task.FromResult(buf);
        }

        public override Task WriteAsync(CorePakFile source, BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception = null)
        {
            var buf = data;
            if (file.Compressed)
                using (var compressor = new Compressor())
                    try
                    {
                        var src = new ArraySegment<byte>(buf);
                        buf = compressor.Wrap(src);
                    }
                    catch (ZstdException e)
                    {
                        exception?.Invoke(file, $"ZstdException: {e.Message}");
                        return Task.CompletedTask;
                    }
            w.Write(buf, 0, buf.Length);
            return Task.CompletedTask;
        }
    }
}