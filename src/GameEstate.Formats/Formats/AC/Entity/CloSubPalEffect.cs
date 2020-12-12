using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class CloSubPalEffect
    {
        /// <summary>
        /// Icon portal.dat 0x06000000
        /// </summary>
        public readonly uint Icon;
        public readonly CloSubPalette[] CloSubPalettes;

        public CloSubPalEffect(BinaryReader r)
        {
            Icon = r.ReadUInt32();
            CloSubPalettes = r.ReadL32Array(x => new CloSubPalette(x));
        }
    }
}
