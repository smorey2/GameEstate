using GameEstate.Core;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class GameTime
    {
        public double ZeroTimeOfYear;
        public uint ZeroYear; // Year "0" is really "P.Y. 10" in the calendar.
        public float DayLength;
        public uint DaysPerYear; // 360. Likely for easier math so each month is same length
        public string YearSpec; // "P.Y."
        public TimeOfDay[] TimesOfDay;
        public string[] DaysOfTheWeek;
        public Season[] Seasons;

        public GameTime(BinaryReader r)
        {
            ZeroTimeOfYear = r.ReadDouble();
            ZeroYear = r.ReadUInt32();
            DayLength = r.ReadSingle();
            DaysPerYear = r.ReadUInt32();
            YearSpec = r.ReadL16String(Encoding.Default);
            r.AlignBoundary();
            TimesOfDay = r.ReadL32Array(x => new TimeOfDay(x));
            DaysOfTheWeek = r.ReadL32Array(x => { var weekDay = r.ReadL16String(); r.AlignBoundary(); return weekDay; });
            Seasons = r.ReadL32Array(x => new Season(x));
        }
    }
}
