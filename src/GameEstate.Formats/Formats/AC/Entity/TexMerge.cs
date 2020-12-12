using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class TexMerge
    {
        public readonly uint BaseTexSize;
        public readonly TerrainAlphaMap[] CornerTerrainMaps;
        public readonly TerrainAlphaMap[] SideTerrainMaps;
        public readonly RoadAlphaMap[] RoadMaps;
        public readonly TMTerrainDesc[] TerrainDesc;

        public TexMerge(BinaryReader r)
        {
            BaseTexSize = r.ReadUInt32();
            CornerTerrainMaps = r.ReadL32Array(x => new TerrainAlphaMap(x));
            SideTerrainMaps = r.ReadL32Array(x => new TerrainAlphaMap(x));
            RoadMaps = r.ReadL32Array(x => new RoadAlphaMap(x));
            TerrainDesc = r.ReadL32Array(x => new TMTerrainDesc(x));
        }
    }
}
