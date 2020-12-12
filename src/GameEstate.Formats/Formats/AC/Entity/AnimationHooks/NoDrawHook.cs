using System.IO;

namespace GameEstate.Formats.AC.Entity.AnimationHooks
{
    public class NoDrawHook : AnimationHook
    {
        public readonly uint NoDraw;

        public NoDrawHook(BinaryReader r) : base(r)
        {
            NoDraw = r.ReadUInt32();
        }
    }
}
