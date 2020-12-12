using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SkyDesc
    {
        public readonly double TickSize;
        public readonly double LightTickSize;
        public readonly DayGroup[] DayGroups;

        public SkyDesc(BinaryReader r)
        {
            TickSize = r.ReadDouble();
            LightTickSize = r.ReadDouble();
            r.AlignBoundary();
            DayGroups = r.ReadL32Array(x => new DayGroup(x));
        }
    }
}
