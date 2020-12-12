using GameEstate.Core;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class CellStruct
    {
        public readonly CVertexArray VertexArray;
        public readonly Dictionary<ushort, Polygon> Polygons;
        public readonly ushort[] Portals;
        public readonly BSPTree CellBSP;
        public readonly Dictionary<ushort, Polygon> PhysicsPolygons;
        public readonly BSPTree PhysicsBSP;
        public readonly BSPTree DrawingBSP;

        public CellStruct(BinaryReader r)
        {
            var numPolygons = r.ReadUInt32();
            var numPhysicsPolygons = r.ReadUInt32();
            var numPortals = r.ReadUInt32();
            VertexArray = new CVertexArray(r);
            Polygons = r.ReadTMany<ushort, Polygon>(sizeof(ushort), x => new Polygon(x), (int)numPolygons);
            Portals = r.ReadTArray<ushort>(sizeof(ushort), (int)numPortals);
            r.AlignBoundary();
            CellBSP = new BSPTree(r, BSPType.Cell);
            PhysicsPolygons = r.ReadTMany<ushort, Polygon>(sizeof(ushort), x => new Polygon(x), (int)numPhysicsPolygons);
            PhysicsBSP = new BSPTree(r, BSPType.Physics);
            var hasDrawingBSP = r.ReadUInt32();
            if (hasDrawingBSP != 0)
                DrawingBSP = new BSPTree(r, BSPType.Drawing);
            r.AlignBoundary();
        }
    }
}
