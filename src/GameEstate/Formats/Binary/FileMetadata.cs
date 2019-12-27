using System.Diagnostics;

namespace GameEstate.Formats.Binary
{
    [DebuggerDisplay("{Path}")]
    public class FileMetadata
    {
        public object Info;
        public string Path;
        public bool Compressed;
        public uint PackedSize;
        public uint FileSize;
        public long Position;
        public object Tag;
    }
}