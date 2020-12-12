using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class TMTerrainDesc
    {
        public readonly uint TerrainType;
        public readonly TerrainTex TerrainTex;

        public TMTerrainDesc(BinaryReader r)
        {
            TerrainType = r.ReadUInt32();
            TerrainTex = new TerrainTex(r);
        }
    }
}
