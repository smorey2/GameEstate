using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    [PakFileType(PakFileType.BadData)]
    public class BadData : FileType
    {
        public const uint FILE_ID = 0x0E00001A;

        // Key is a list of a WCIDs that are "bad" and should not exist. The value is always 1 (could be a bool?)
        public Dictionary<uint, uint> Bad = new Dictionary<uint, uint>();

        public override void Read(BinaryReader r)
        {
            Id = r.ReadUInt32();
            Bad.UnpackPackedHashTable(r);
        }
    }
}
