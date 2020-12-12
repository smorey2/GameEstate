using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    // TODO: refactor to merge with existing TextureMapOverride object
    public class TextureMapChange
    {
        public readonly byte PartIndex;
        public readonly uint OldTexture;
        public readonly uint NewTexture;

        public TextureMapChange(BinaryReader r)
        {
            PartIndex = r.ReadByte();
            OldTexture = r.ReadAsDataIDOfKnownType(0x05000000);
            NewTexture = r.ReadAsDataIDOfKnownType(0x05000000);
        }
    }
}
