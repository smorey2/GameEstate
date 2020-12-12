using System.IO;

namespace GameEstate.Formats.AC.FileTypes
{
    public abstract class FileType
    {
        public uint Id { get; protected set; }
        public abstract void Read(BinaryReader r);
    }
}
