using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SoundDesc
    {
        public readonly AmbientSTBDesc[] STBDesc;

        public SoundDesc(BinaryReader r)
        {
            STBDesc = r.ReadL32Array(x => new AmbientSTBDesc(x));
        }
    }
}
