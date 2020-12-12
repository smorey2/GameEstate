using System.IO;

namespace GameEstate.Formats.AC.Entity.AnimationHooks
{
    public class SoundHook : AnimationHook
    {
        public readonly uint Id;

        public SoundHook(BinaryReader r) : base(r)
        {
            Id = r.ReadUInt32();
        }
    }
}
