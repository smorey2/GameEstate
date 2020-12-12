using System;
using System.IO;
using System.Linq;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class BSPLeaf : BSPNode
    {
        public readonly int LeafIndex;
        public readonly int Solid;

        public BSPLeaf(BinaryReader r, BSPType treeType)
        {
            Type = Encoding.ASCII.GetString(r.ReadBytes(4)).Reverse();
            LeafIndex = r.ReadInt32();
            if (treeType == BSPType.Physics)
            {
                Solid = r.ReadInt32();
                // Note that if Solid is equal to 0, these values will basically be null. Still read them, but they don't mean anything.
                Sphere = new Sphere(r);
                InPolys = r.ReadL32Array<ushort>(sizeof(ushort));
            }
        }
    }
}
