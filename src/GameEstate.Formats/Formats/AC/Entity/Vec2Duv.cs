using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    /// <summary>
    /// Info on texture UV mapping
    /// </summary>
    public class Vec2Duv
    {
        public readonly float U;
        public readonly float V;

        public Vec2Duv(BinaryReader r)
        {
            U = r.ReadSingle();
            V = r.ReadSingle();
        }
    }
}
