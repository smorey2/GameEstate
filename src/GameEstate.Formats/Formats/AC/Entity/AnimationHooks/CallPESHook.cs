using System.IO;

namespace GameEstate.Formats.AC.Entity.AnimationHooks
{
    public class CallPESHook : AnimationHook
    {
        public readonly uint PES;
        public readonly float Pause;

        public CallPESHook(BinaryReader r) : base(r)
        {
            PES = r.ReadUInt32();
            Pause = r.ReadSingle();
        }
    }
}
