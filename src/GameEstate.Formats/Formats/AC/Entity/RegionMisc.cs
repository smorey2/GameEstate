using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class RegionMisc
    {
        public readonly uint Version;
        public readonly uint GameMapID;
        public readonly uint AutotestMapId;
        public readonly uint AutotestMapSize;
        public readonly uint ClearCellId;
        public readonly uint ClearMonsterId;

        public RegionMisc(BinaryReader r)
        {
            Version = r.ReadUInt32();
            GameMapID = r.ReadUInt32();
            AutotestMapId = r.ReadUInt32();
            AutotestMapSize = r.ReadUInt32();
            ClearCellId = r.ReadUInt32();
            ClearMonsterId = r.ReadUInt32();
        }
    }
}
