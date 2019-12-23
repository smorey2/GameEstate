using System;
using System.IO;
using System.Threading.Tasks;
using ZstdNet;

namespace GameEstate.Core.DataFormat
{
    public static class DatFormat01
    {
        public static Task<byte[]> Read(CorePakFile source, BinaryReader r, FileMetadata file)
        {
            var buf = new byte[file.PackedSize];
            r.Position(file.Position);
            r.Read(buf, 0, buf.Length);
            if (file.Compressed)
                using (var decompressor = new Decompressor())
                {
                    try { return Task.FromResult(decompressor.Unwrap(buf)); }
                    catch (ZstdException e)
                    {
                        Console.WriteLine($"Skipping the following file because it is broken. Size: {file.PackedSize}");
                        Console.WriteLine($"Error: {e.Message}");
                        return null;
                    }
                }
            return Task.FromResult(buf);
        }

        public static void Write(CorePakFile source, BinaryWriter w, FileMetadata file, byte[] data) => throw new NotSupportedException();
    }
}