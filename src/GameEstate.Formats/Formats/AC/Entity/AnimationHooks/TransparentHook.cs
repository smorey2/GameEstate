using System.IO;

namespace GameEstate.Formats.AC.Entity.AnimationHooks
{
    public class TransparentHook : AnimationHook
    {
        public readonly float Start;
        public readonly float End;
        public readonly float Time;

        public TransparentHook(BinaryReader r) : base(r)
        {
            Start = r.ReadSingle();
            End = r.ReadSingle();
            Time = r.ReadSingle();
        }
    }
}
