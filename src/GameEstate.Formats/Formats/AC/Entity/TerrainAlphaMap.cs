using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class TerrainAlphaMap
    {
        public readonly uint TCode;
        public readonly uint TexGID;

        public TerrainAlphaMap(BinaryReader r)
        {
            TCode = r.ReadUInt32();
            TexGID = r.ReadUInt32();
        }
    }
}
