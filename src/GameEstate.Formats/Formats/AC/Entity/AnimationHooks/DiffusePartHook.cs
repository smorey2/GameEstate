using System.IO;

namespace GameEstate.Formats.AC.Entity.AnimationHooks
{
    public class DiffusePartHook : AnimationHook
    {
        public readonly uint Part;
        public readonly float Start;
        public readonly float End;
        public readonly float Time;

        public DiffusePartHook(BinaryReader r) : base(r)
        {
            Part = r.ReadUInt32();
            Start = r.ReadSingle();
            End = r.ReadSingle();
            Time = r.ReadSingle();
        }
    }
}
