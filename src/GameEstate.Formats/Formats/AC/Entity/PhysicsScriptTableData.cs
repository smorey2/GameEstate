using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class PhysicsScriptTableData
    {
        public readonly ScriptAndModData[] Scripts;

        public PhysicsScriptTableData(BinaryReader r)
        {
            Scripts = r.ReadL32Array(x => new ScriptAndModData(r));
        }
    }
}
