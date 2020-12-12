using System.Collections.Generic;
using System.IO;

using ACE.DatLoader.Entity;

namespace GameEstate.Formats.AC.FileTypes
{
    /// <summary>
    /// This is the client_portal.dat file 0x0E00001D
    /// </summary>
    [PakFileType(PakFileType.ContractTable)]
    public class ContractTable : FileType
    {
        public const uint FILE_ID = 0x0E00001D;

        public Dictionary<uint, Contract> Contracts { get; } = new Dictionary<uint, Contract>();

        public override void Read(BinaryReader reader)
        {
            Id = reader.ReadUInt32();

            ushort num_contracts = reader.ReadUInt16();
            /*ushort table_size = */reader.ReadUInt16(); // We don't need this since C# handles it's own memory

            for (ushort i = 0; i < num_contracts; i++)
            {
                uint key = reader.ReadUInt32();

                Contract value = new Contract();
                value.Unpack(reader);

                Contracts.Add(key, value);
            }
        }
    }
}
