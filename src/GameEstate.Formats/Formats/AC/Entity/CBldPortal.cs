using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class CBldPortal
    {
        public readonly PortalFlags Flags;

        // Not sure what these do. They are both calculated from the flags.
        public bool ExactMatch => Flags.HasFlag(PortalFlags.ExactMatch);
        public bool PortalSide => Flags.HasFlag(PortalFlags.PortalSide);

        // Basically the cells that connect both sides of the portal
        public readonly ushort OtherCellId;
        public readonly ushort OtherPortalId;

        /// <summary>
        /// List of cells used in this structure. (Or possibly just those visible through it.)
        /// </summary>
        public readonly ushort[] StabList;

        public CBldPortal(BinaryReader r)
        {
            Flags = (PortalFlags)r.ReadUInt16();
            OtherCellId = r.ReadUInt16();
            OtherPortalId = r.ReadUInt16();
            StabList = r.ReadL16Array<ushort>(sizeof(ushort));
            r.AlignBoundary();
        }
    }
}
