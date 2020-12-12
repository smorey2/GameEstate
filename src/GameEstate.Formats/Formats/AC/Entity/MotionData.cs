using GameEstate.Core;
using System.IO;
using System.Numerics;

namespace GameEstate.Formats.AC.Entity
{
    public class MotionData
    {
        public readonly byte Bitfield;
        public readonly MotionDataFlags Flags;
        public readonly AnimData[] Anims;
        public readonly Vector3 Velocity;
        public readonly Vector3 Omega;

        public MotionData(BinaryReader r)
        {
            var numAnims = r.ReadByte();
            Bitfield = r.ReadByte();
            Flags = (MotionDataFlags)r.ReadByte();
            r.AlignBoundary();
            Anims = r.ReadL32Array(x => new AnimData(x), numAnims);
            if ((Flags & MotionDataFlags.HasVelocity) != 0)
                Velocity = r.ReadVector3();
            if ((Flags & MotionDataFlags.HasOmega) != 0)
                Omega = r.ReadVector3();
        }
    }
}
