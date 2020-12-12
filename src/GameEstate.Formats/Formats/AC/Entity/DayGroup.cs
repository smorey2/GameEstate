using GameEstate.Core;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class DayGroup
    {
        public readonly float ChanceOfOccur;
        public readonly string DayName;
        public readonly SkyObject[] SkyObjects;
        public readonly SkyTimeOfDay[] SkyTime;

        public DayGroup(BinaryReader r)
        {
            ChanceOfOccur = r.ReadSingle();
            DayName = r.ReadL16String(Encoding.Default);
            r.AlignBoundary();
            SkyObjects = r.ReadL32Array(x => new SkyObject(x));
            SkyTime = r.ReadL32Array(x => new SkyTimeOfDay(x));
        }
    }
}
