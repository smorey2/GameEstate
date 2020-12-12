using System.IO;

namespace GameEstate.Formats.AC.Entity.AnimationHooks
{
    public class StopParticleHook : AnimationHook
    {
        public readonly uint EmitterId;

        public StopParticleHook(BinaryReader r) : base(r)
        {
            EmitterId = r.ReadUInt32();
        }
    }
}
