using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class ObjectDesc
    {
        public readonly uint ObjId;
        public readonly Frame BaseLoc;
        public readonly float Freq;
        public readonly float DisplaceX;
        public readonly float DisplaceY;
        public readonly float MinScale;
        public readonly float MaxScale;
        public readonly float MaxRotation;
        public readonly float MinSlope;
        public readonly float MaxSlope;
        public readonly uint Align;
        public readonly uint Orient;
        public readonly uint WeenieObj;

        public ObjectDesc(BinaryReader r)
        {
            ObjId = r.ReadUInt32();

            BaseLoc = new Frame(r);

            Freq = r.ReadSingle();

            DisplaceX = r.ReadSingle();
            DisplaceY = r.ReadSingle();

            MinScale = r.ReadSingle();
            MaxScale = r.ReadSingle();

            MaxRotation = r.ReadSingle();

            MinSlope = r.ReadSingle();
            MaxSlope = r.ReadSingle();

            Align = r.ReadUInt32();
            Orient = r.ReadUInt32();

            WeenieObj = r.ReadUInt32();
        }
    }
}
