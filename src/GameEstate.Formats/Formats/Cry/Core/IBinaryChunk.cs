using System.IO;

namespace GameEstate.Formats.Cry.Core
{
    public interface IBinaryChunk
    {
        void Read(BinaryReader reader);
        void Write(BinaryWriter writer);
    }
}
