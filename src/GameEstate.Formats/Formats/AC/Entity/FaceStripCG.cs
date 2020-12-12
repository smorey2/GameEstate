using System.IO;

namespace GameEstate.Formats.AC.Entity
{
    public class FaceStripCG
    {
        public readonly uint IconImage;
        public readonly ObjDesc ObjDesc;

        public FaceStripCG(BinaryReader r)
        {
            IconImage = r.ReadUInt32();
            ObjDesc = new ObjDesc(r);
        }
    }
}
