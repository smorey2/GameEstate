using GameEstate.Core;
using System;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class BSPNode
    {
        // These constants are actually strings in the dat file
        const uint PORT = 1347375700; // 0x504F5254
        const uint LEAF = 1279607110; // 0x4C454146
        const uint BPnn = 1112567406; // 0x42506E6E
        const uint BPIn = 1112557934; // 0x4250496E
        const uint BpIN = 1114655054; // 0x4270494E
        const uint BpnN = 1114664526; // 0x42706E4E
        const uint BPIN = 1112557902; // 0x4250494E
        const uint BPnN = 1112567374; // 0x42506E4E

        public readonly string Type;
        public readonly Plane SplittingPlane;
        public readonly BSPNode PosNode;
        public readonly BSPNode NegNode;
        public Sphere Sphere;
        public ushort[] InPolys; // List of PolygonIds

        public BSPNode(BinaryReader r, BSPType treeType)
        {
            Type = Encoding.ASCII.GetString(r.ReadBytes(4)).Reverse();
            switch (Type)
            {
                // These types will unpack the data completely, in their own classes
                case "PORT":
                case "LEAF":
                    throw new Exception();
            }
            SplittingPlane = new Plane(r);
            switch (Type)
            {
                case "BPnn":
                case "BPIn": PosNode = Factory(r, treeType); break;
                case "BpIN":
                case "BpnN": NegNode = Factory(r, treeType); break;
                case "BPIN":
                case "BPnN":
                    PosNode = Factory(r, treeType);
                    NegNode = Factory(r, treeType);
                    break;
            }
            if (treeType == BSPType.Cell)
                return;
            Sphere = new Sphere(r);
            if (treeType == BSPType.Physics)
                return;
            InPolys = r.ReadL32Array<ushort>(sizeof(ushort));
        }

        public static BSPNode Factory(BinaryReader r, BSPType treeType)
        {
            // We peek forward to get the type, then revert our position.
            var type = Encoding.ASCII.GetString(r.ReadBytes(4)).Reverse();
            r.BaseStream.Position -= 4;
            switch (type)
            {
                case "PORT": return new BSPPortal(r, treeType);
                case "LEAF": return new BSPLeaf(r, treeType);
                case "BPnn":
                case "BPIn":
                case "BpIN":
                case "BpnN":
                case "BPIN":
                case "BPnN":
                default: return new BSPNode(r, treeType);
            }
        }
    }
}
