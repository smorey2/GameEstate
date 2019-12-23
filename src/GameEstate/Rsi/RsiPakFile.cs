using System;
using System.IO;
using System.Threading.Tasks;
using GameEstate.Core;
using GameEstate.Core.DataFormat;

namespace GameEstate.Rsi
{
    public class RsiPakFile : CorePakFile
    {
        public RsiPakFile(string filePath) : base(filePath) { }

        static readonly byte[] Magic = { 0x50, 0x4B, 0x03, 0x14 };

        protected override Task<byte[]> LoadFileDataAsync(FileMetadata file) => throw new NotImplementedException();
        protected override void ReadMetadata(BinaryReader r) => PakFormat01.Read(this, r, Magic);
        protected override void Write(BinaryWriter w) => PakFormat01.Write(this, w, Magic);
    }
}