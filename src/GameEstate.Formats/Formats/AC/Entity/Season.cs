using GameEstate.Core;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class Season
    {
        public readonly uint StartDate;
        public readonly string Name;

        public Season(BinaryReader r)
        {
            StartDate = r.ReadUInt32();
            Name = r.ReadL16String(Encoding.Default);
            r.AlignBoundary();
        }
    }
}
