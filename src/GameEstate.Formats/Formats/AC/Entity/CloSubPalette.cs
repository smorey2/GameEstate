using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class CloSubPalette
    {
        /// <summary>
        /// Contains a list of valid offsets & color values
        /// </summary>
        public readonly CloSubPaletteRange[] Ranges;
        /// <summary>
        /// Icon portal.dat 0x0F000000
        /// </summary>
        public readonly uint PaletteSet;

        public CloSubPalette(BinaryReader r)
        {
            Ranges = r.ReadL32Array(x => new CloSubPaletteRange(x));
            PaletteSet = r.ReadUInt32();
        }
    }
}
