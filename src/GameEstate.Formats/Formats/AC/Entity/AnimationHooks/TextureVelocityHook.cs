using System.IO;

namespace GameEstate.Formats.AC.Entity.AnimationHooks
{
    public class TextureVelocityHook : AnimationHook
    {
        public readonly float USpeed;
        public readonly float VSpeed;

        public TextureVelocityHook(BinaryReader r) : base(r)
        {
            USpeed = r.ReadSingle();
            VSpeed = r.ReadSingle();
        }
    }
}
