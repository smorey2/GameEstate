using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class PhysicsScriptData
    {
        public readonly double StartTime;
        public readonly AnimationHook Hook;

        public PhysicsScriptData(BinaryReader r)
        {
            StartTime = r.ReadDouble();
            Hook = AnimationHook.Factory(r);
        }
    }
}
