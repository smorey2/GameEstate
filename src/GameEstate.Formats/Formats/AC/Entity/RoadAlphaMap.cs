using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class RoadAlphaMap
    {
        public readonly uint RCode;
        public readonly uint RoadTexGID;

        public RoadAlphaMap(BinaryReader r)
        {
            RCode = r.ReadUInt32();
            RoadTexGID = r.ReadUInt32();
        }
    }
}
