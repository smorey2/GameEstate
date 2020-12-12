using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class TerrainTex
    {
        public readonly uint TexGID;
        public readonly uint TexTiling;
        public readonly uint MaxVertBright;
        public readonly uint MinVertBright;
        public readonly uint MaxVertSaturate;
        public readonly uint MinVertSaturate;
        public readonly uint MaxVertHue;
        public readonly uint MinVertHue;
        public readonly uint DetailTexTiling;
        public readonly uint DetailTexGID;

        public TerrainTex(BinaryReader r)
        {
            TexGID = r.ReadUInt32();
            TexTiling = r.ReadUInt32();
            MaxVertBright = r.ReadUInt32();
            MinVertBright = r.ReadUInt32();
            MaxVertSaturate = r.ReadUInt32();
            MinVertSaturate = r.ReadUInt32();
            MaxVertHue = r.ReadUInt32();
            MinVertHue = r.ReadUInt32();
            DetailTexTiling = r.ReadUInt32();
            DetailTexGID = r.ReadUInt32();
        }
    }
}
