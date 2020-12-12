using GameEstate.Core;
using System;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class BSPPortal : BSPNode
    {
        public readonly PortalPoly[] InPortals;

        public BSPPortal(BinaryReader r, BSPType treeType)
        {
            Type = Encoding.ASCII.GetString(r.ReadBytes(4)).Reverse();
            SplittingPlane = new Plane(r);
            PosNode = BSPNode.ReadNode(r, treeType);
            NegNode = BSPNode.ReadNode(r, treeType);
            if (treeType == BSPType.Drawing)
            {
                Sphere = new Sphere(r);
                var numPolys = r.ReadUInt32();
                var numPortals = r.ReadUInt32();
                InPolys = r.ReadTArray<ushort>(sizeof(ushort), (int)numPolys);
                InPortals = r.ReadTArray(x => new PortalPoly(x), (int)numPortals);
            }
        }
    }
}
