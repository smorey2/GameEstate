using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class ObjDesc
    {
        public readonly uint PaletteID;
        public readonly SubPalette[] SubPalettes;
        public readonly TextureMapChange[] TextureChanges;
        public readonly AnimationPartChange[] AnimPartChanges;

        public ObjDesc(BinaryReader r)
        {
            r.AlignBoundary();
            r.ReadByte(); // ObjDesc always starts with 11.
            var numPalettes = r.ReadByte();
            var numTextureMapChanges = r.ReadByte();
            var numAnimPartChanges = r.ReadByte();
            if (numPalettes > 0)
                PaletteID = r.ReadAsDataIDOfKnownType(0x04000000);
            SubPalettes = r.ReadTArray(x => new SubPalette(x), numPalettes);
            TextureChanges = r.ReadTArray(x => new TextureMapChange(x), numTextureMapChanges);
            AnimPartChanges = r.ReadTArray(x => new AnimationPartChange(x), numAnimPartChanges);
            r.AlignBoundary();
        }
    }
}
