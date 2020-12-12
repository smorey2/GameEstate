using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class CellPortal
    {
        public readonly PortalFlags Flags;
        public readonly ushort PolygonId;
        public readonly ushort OtherCellId;
        public readonly ushort OtherPortalId;
        public bool ExactMatch => (Flags & PortalFlags.ExactMatch) != 0;
        public bool PortalSide => (Flags & PortalFlags.PortalSide) == 0;

        public CellPortal(BinaryReader r)
        {
            Flags = (PortalFlags)r.ReadUInt16();
            PolygonId = r.ReadUInt16();
            OtherCellId = r.ReadUInt16();
            OtherPortalId = r.ReadUInt16();
        }
    }
}
