using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class CloSubPaletteRange
    {
        public readonly uint Offset;
        public readonly uint NumColors;

        public CloSubPaletteRange(BinaryReader r)
        {
            Offset = r.ReadUInt32();
            NumColors = r.ReadUInt32();
        }
    }
}
