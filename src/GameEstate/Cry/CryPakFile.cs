using GameEstate.Core;
using GameEstate.Core.DataFormat;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Cry
{
    public class CryPakFile : CorePakFile
    {
        public CryPakFile(string filePath) : base(filePath) { }

        protected override Task<byte[]> LoadFileDataAsync(FileMetadata file) => throw new NotImplementedException();
        protected override void ReadMetadata(BinaryReader r) => throw new NotImplementedException();
        protected override void Write(BinaryWriter w) => throw new NotImplementedException();
    }
}