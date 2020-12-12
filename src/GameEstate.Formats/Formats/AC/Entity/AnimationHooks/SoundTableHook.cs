using System.IO;

namespace GameEstate.Formats.AC.Entity.AnimationHooks
{
    public class SoundTableHook : AnimationHook
    {
        public readonly uint SoundType;

        public SoundTableHook(BinaryReader r) : base(r)
        {
            SoundType = r.ReadUInt32();
        }
    }
}
