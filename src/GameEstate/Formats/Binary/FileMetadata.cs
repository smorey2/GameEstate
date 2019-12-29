using GameEstate.Core;
using System.Diagnostics;

namespace GameEstate.Formats.Binary
{
    [DebuggerDisplay("{Path}")]
    public class FileMetadata
    {
        public string Path;
        public bool Compressed;
        public bool Crypted;
        public long PackedSize;
        public long FileSize;
        public long Position;
        // extra
        public object Info;
        public int ExtraSize;
        public long ExtraPosition;
        public CorePakFile Pak;
        public object Tag;
    }
}