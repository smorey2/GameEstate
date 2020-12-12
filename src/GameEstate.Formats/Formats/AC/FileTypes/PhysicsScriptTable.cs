using System.Collections.Generic;
using System.IO;

using ACE.DatLoader.Entity;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// These are client_portal.dat files starting with 0x34. 
    /// </summary>
    [PakFileType(PakFileType.PhysicsScriptTable)]
    public class PhysicsScriptTable : FileType
    {
        public Dictionary<uint, PhysicsScriptTableData> ScriptTable { get; set; } = new Dictionary<uint, PhysicsScriptTableData>();

        public override void Read(BinaryReader reader)
        {
            Id = reader.ReadUInt32();

            ScriptTable.Unpack(reader);
        }
    }
}
