using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SkyObject
    {
        public readonly float BeginTime;
        public readonly float EndTime;
        public readonly float BeginAngle;
        public readonly float EndAngle;
        public readonly float TexVelocityX;
        public readonly float TexVelocityY;
        public readonly float TexVelocityZ = 0;
        public readonly uint DefaultGFXObjectId;
        public readonly uint DefaultPESObjectId;
        public readonly uint Properties;

        public SkyObject(BinaryReader r)
        {
            BeginTime = r.ReadSingle();
            EndTime = r.ReadSingle();
            BeginAngle = r.ReadSingle();
            EndAngle = r.ReadSingle();
            TexVelocityX = r.ReadSingle();
            TexVelocityY = r.ReadSingle();
            DefaultGFXObjectId = r.ReadUInt32();
            DefaultPESObjectId = r.ReadUInt32();
            Properties = r.ReadUInt32();
            r.AlignBoundary();
        }
    }
}
