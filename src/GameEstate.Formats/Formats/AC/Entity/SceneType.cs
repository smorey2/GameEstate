using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class SceneType
    {
        public uint StbIndex;
        public uint[] Scenes;

        public SceneType(BinaryReader r)
        {
            StbIndex = r.ReadUInt32();
            Scenes = r.ReadL32Array<uint>(sizeof(uint));
        }
    }
}
