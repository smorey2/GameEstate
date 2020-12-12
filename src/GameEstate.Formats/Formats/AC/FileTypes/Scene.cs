using System.Collections.Generic;
using System.IO;

using ACE.DatLoader.Entity;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// These are client_portal.dat files starting with 0x12. 
    /// </summary>
    [PakFileType(PakFileType.Scene)]
    public class Scene : FileType
    {
        public List<ObjectDesc> Objects { get; } = new List<ObjectDesc>();

        public override void Read(BinaryReader reader)
        {
            Id = reader.ReadUInt32();

            Objects.Unpack(reader);
        }
    }
}
