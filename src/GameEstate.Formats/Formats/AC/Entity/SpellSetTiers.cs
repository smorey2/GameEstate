using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SpellSetTiers
    {
        /// <summary>
        /// A list of spell ids that are active in the spell set tier
        /// </summary>
        public readonly uint[] Spells;

        public SpellSetTiers(BinaryReader r)
        {
            Spells = r.ReadL32Array<uint>(sizeof(uint));
        }
    }
}
