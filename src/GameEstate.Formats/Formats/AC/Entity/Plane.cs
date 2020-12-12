using GameEstate.Core;
using System.IO;
using System.Numerics;

namespace GameEstate.Formats.AC.Entity
{
    public class Plane
    {
        public readonly Vector3 N;
        public readonly float D;

        public Plane(BinaryReader r)
        {
            N = r.ReadVector3();
            D = r.ReadSingle();
        }

        public System.Numerics.Plane ToNumerics() => new System.Numerics.Plane(N, D);
    }
}
