using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class AmbientSTBDesc
    {
        public readonly uint STBId;
        public readonly AmbientSoundDesc[] AmbientSounds;

        public AmbientSTBDesc(BinaryReader r)
        {
            STBId = r.ReadUInt32();
            AmbientSounds = r.ReadL32Array(x => new AmbientSoundDesc(x));
        }
    }
}
