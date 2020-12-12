using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class BuildInfo
    {
        /// <summary>
        /// 0x01 or 0x02 model of the building
        /// </summary>
        public readonly uint ModelId;
        /// <summary>
        /// specific @loc of the model
        /// </summary>
        public readonly Frame Frame;
        /// <summary>
        /// unsure what this is used for
        /// </summary>
        public readonly uint NumLeaves;
        /// <summary>
        /// portals are things like doors, windows, etc.
        /// </summary>
        public CBldPortal[] Portals;

        public BuildInfo(BinaryReader r)
        {
            ModelId = r.ReadUInt32();
            Frame = new Frame(r);
            NumLeaves = r.ReadUInt32();
            Portals = r.ReadL32Array(x => new CBldPortal(x));
        }
    }
}
