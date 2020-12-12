using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class GfxObjInfo
    {
        public readonly uint Id;
        public readonly uint DegradeMode;
        public readonly float MinDist;
        public readonly float IdealDist;
        public readonly float MaxDist;

        public GfxObjInfo(BinaryReader r)
        {
            Id = r.ReadUInt32();
            DegradeMode = r.ReadUInt32();
            MinDist = r.ReadSingle();
            IdealDist = r.ReadSingle();
            MaxDist = r.ReadSingle();
        }
    }
}
