using GameEstate.Core;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class TimeOfDay
    {
        public readonly float Start;
        public readonly bool IsNight;
        public readonly string Name;

        public TimeOfDay(BinaryReader r)
        {
            Start = r.ReadSingle();
            IsNight = r.ReadUInt32() == 1;
            Name = r.ReadL16String(Encoding.Default);
            r.AlignBoundary();
        }
    }
}
