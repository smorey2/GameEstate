using System.Numerics;

namespace GameEstate.Toy.Models.Animations
{
    public class FrameBone
    {
        public FrameBone(Vector3 pos, Quaternion a)
        {
            Position = pos;
            Angle = a;
        }

        public Vector3 Position { get; set; }
        public Quaternion Angle { get; set; }
    }
}
