using GameEstate.Core;
using System.IO;
using System.Text;

namespace GameEstate.Formats.AC.Entity
{
    public class TerrainType
    {
        public readonly string TerrainName;
        public readonly uint TerrainColor;
        public readonly uint[] SceneTypes;

        public TerrainType(BinaryReader r)
        {
            TerrainName = r.ReadL16String(Encoding.Default);
            r.AlignBoundary();
            TerrainColor = r.ReadUInt32();
            SceneTypes = r.ReadL32Array<uint>(sizeof(uint));
        }
    }
}
