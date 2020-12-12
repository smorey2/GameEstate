using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    // TODO: refactor to use existing PaletteOverride object
    public class SubPalette
    {
        public readonly uint SubID;
        public readonly uint Offset;
        public readonly uint NumColors;

        public SubPalette(BinaryReader r)
        {
            SubID = r.ReadAsDataIDOfKnownType(0x04000000);
            Offset = (uint)(r.ReadByte() * 8);
            NumColors = r.ReadByte();
            if (NumColors == 0)
                NumColors = 256;
            NumColors *= 8;
        }
    }
}
