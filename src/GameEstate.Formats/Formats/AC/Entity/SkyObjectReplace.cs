using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SkyObjectReplace
    {
        public readonly uint ObjectIndex;
        public readonly uint GFXObjId;
        public readonly float Rotate;
        public readonly float Transparent;
        public readonly float Luminosity;
        public readonly float MaxBright;

        public SkyObjectReplace(BinaryReader r)
        {
            ObjectIndex = r.ReadUInt32();
            GFXObjId = r.ReadUInt32();
            Rotate = r.ReadSingle();
            Transparent = r.ReadSingle();
            Luminosity = r.ReadSingle();
            MaxBright = r.ReadSingle();
            r.AlignBoundary();
        }
    }
}
