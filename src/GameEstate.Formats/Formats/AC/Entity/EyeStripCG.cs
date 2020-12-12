using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class EyeStripCG
    {
        public readonly uint IconImage;
        public readonly uint IconImageBald;
        public readonly ObjDesc ObjDesc;
        public readonly ObjDesc ObjDescBald;

        public EyeStripCG(BinaryReader r)
        {
            IconImage = r.ReadUInt32();
            IconImageBald = r.ReadUInt32();
            ObjDesc = new ObjDesc(r);
            ObjDescBald = new ObjDesc(r);
        }
    }
}
