using System.IO;
using GameEstate.Core;

namespace GameEstate.Cry
{
    public class CryPakFile : CorePakFile
    {
        public CryPakFile(string filePath) : base(filePath) { }

        static readonly byte[] HeaderMagic = { 0x50, 0x4B, 0x03, 0x14 };

        protected override void ReadMetadata(BinaryReader r)
        {
            ReadOne(r, x => x[0] == HeaderMagic[0] && x[1] == HeaderMagic[1] && x[2] == HeaderMagic[2] && x[3] == HeaderMagic[3]);
        }
    }
}