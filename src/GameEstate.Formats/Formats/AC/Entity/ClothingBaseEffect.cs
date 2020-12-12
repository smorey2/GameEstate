using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class ClothingBaseEffect
    {
        public readonly CloObjectEffect[] CloObjectEffects;

        public ClothingBaseEffect(BinaryReader r)
        {
            CloObjectEffects = r.ReadL32Array(x => new CloObjectEffect(x));
        }
    }
}
