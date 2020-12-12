using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class CloObjectEffect
    {
        public readonly uint Index;
        public readonly uint ModelId;
        public readonly CloTextureEffect[] CloTextureEffects;

        public CloObjectEffect(BinaryReader r)
        {
            Index = r.ReadUInt32();
            ModelId = r.ReadUInt32();
            CloTextureEffects = r.ReadL32Array(x => new CloTextureEffect(x));
        }
    }
}
