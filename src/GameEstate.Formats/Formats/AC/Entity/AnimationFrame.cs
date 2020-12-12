using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class AnimationFrame
    {
        public readonly Frame[] Frames;
        public readonly AnimationHook[] Hooks;

        public AnimationFrame(BinaryReader r, uint numParts)
        {
            Frames = r.ReadTArray(x => new Frame(r), (int)numParts);
            Hooks = r.ReadL32Array(AnimationHook.Factory);
        }
    }
}
