using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class TerrainDesc
    {
        public readonly TerrainType[] TerrainTypes;
        public readonly LandSurf LandSurfaces;

        public TerrainDesc(BinaryReader r)
        {
            TerrainTypes = r.ReadL32Array(x => new TerrainType(x));
            LandSurfaces = new LandSurf(r);
        }
    }
}
