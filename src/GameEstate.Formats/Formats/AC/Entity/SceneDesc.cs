using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SceneDesc
    {
        public readonly SceneType[] SceneTypes;

        public SceneDesc(BinaryReader r)
        {
            SceneTypes = r.ReadL32Array(x => new SceneType(x));
        }
    }
}
