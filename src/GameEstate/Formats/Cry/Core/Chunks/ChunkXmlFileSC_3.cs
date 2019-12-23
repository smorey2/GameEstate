using System.IO;

namespace GameEstate.Formats.Cry.Core
{
    public class ChunkXmlFileSC_3 : ChunkXmlFileSC
    {
        public override void Read(BinaryReader r)
        {
            base.Read(r);
            var xml = CryXmlSerializer.ReadStream(r.BaseStream, true);
        }
    }
}
