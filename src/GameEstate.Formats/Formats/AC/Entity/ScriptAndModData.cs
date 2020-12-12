using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class ScriptAndModData
    {
        public readonly float Mod;
        public readonly uint ScriptId;

        public ScriptAndModData(BinaryReader r)
        {
            Mod = r.ReadSingle();
            ScriptId = r.ReadUInt32();
        }
    }
}
