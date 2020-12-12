using GameEstate.Core;
using System.IO;
using System.Numerics;

namespace GameEstate.Formats.AC.Entity
{
    public class CylSphere
    {
        public readonly Vector3 Origin;
        public readonly float Radius;
        public readonly float Height;

        public CylSphere(BinaryReader r)
        {
            Origin = r.ReadVector3();
            Radius = r.ReadSingle();
            Height = r.ReadSingle();
        }
    }
}
