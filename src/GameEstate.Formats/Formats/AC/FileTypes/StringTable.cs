using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    [PakFileType(PakFileType.StringTable)]
    public class StringTable : FileType
    {
        public static uint CharacterTitle_FileID = 0x2300000E;

        public uint Language { get; private set; } // This should always be 1 for English
        public byte Unknown { get; private set; }
        public List<StringTableData> StringTableData { get; } = new List<StringTableData>();

        public override void Read(BinaryReader r)
        {
            Id = r.ReadUInt32();
            Language = r.ReadUInt32();
            Unknown = r.ReadByte();
            StringTableData.UnpackSmartArray(r);
        }
    }
}
