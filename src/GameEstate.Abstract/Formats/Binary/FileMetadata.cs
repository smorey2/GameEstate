using GameEstate.Core;
using System.Diagnostics;

namespace GameEstate.Formats.Binary
{
    [DebuggerDisplay("{Path}")]
    public class FileMetadata
    {
        public int Id;
        public string Path;
        public bool Compressed;
        public bool Crypted;
        public long PackedSize;
        public long FileSize;
        public long Position;
        public long Digest;
        // extra
        public byte[] Extra;
        public object Info;
        public BinaryPakFile Pak;
        public object Tag;
    }
}