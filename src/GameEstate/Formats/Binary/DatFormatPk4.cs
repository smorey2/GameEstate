using GameEstate.Core;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class DatFormatPk4 : DatFormat
    {
        public override Task<byte[]> ReadAsync(CorePakFile source, BinaryReader r, FileMetadata file, Action<FileMetadata, string> exception = null)
        {
            var pak = (ZipFile)source.Tag;
            var entry = (ZipEntry)file.Tag;
            var stream = pak.GetInputStream(entry);
            if (!stream.CanRead)
            {
                exception?.Invoke(file, $"Stream Closed.");
                return null;
            }
            try
            {
                using (var s = pak.GetInputStream(entry))
                    return Task.FromResult(s.ReadAllBytes());
            }
            catch (Exception e)
            {
                exception?.Invoke(file, $"Exception: {e.Message}");
                return null;
            }
        }

        public override Task WriteAsync(CorePakFile source, BinaryWriter w, FileMetadata file, byte[] data, Action<FileMetadata, string> exception = null)
        {
            return Task.CompletedTask;
        }
    }
}