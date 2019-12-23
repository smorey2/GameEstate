using System.Diagnostics;

namespace GameEstate.Core.DataFormat
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