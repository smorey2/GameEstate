using GameEstate.Core;
using System.IO;
using System.Numerics;

namespace GameEstate.Formats.AC.Entity
{
    public class Sphere
    {
        public static Sphere Empty = new Sphere();
        public readonly Vector3 Origin;
        public readonly float Radius;

        Sphere() { Origin = Vector3.Zero; }
        public Sphere(BinaryReader r)
        {
            Origin = r.ReadVector3();
            Radius = r.ReadSingle();
        }

        public override string ToString() => $"Origin: {Origin}, Radius: {Radius}";
    }
}
