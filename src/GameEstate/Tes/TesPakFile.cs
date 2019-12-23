using System;
using System.IO;
using System.Threading.Tasks;
using GameEstate.Core;
using GameEstate.Core.DataFormat;

namespace GameEstate.Tes
{
    public class TesPakFile : CorePakFile
    {
        public TesPakFile(string filePath) : base(filePath) { }

        protected override Task<byte[]> LoadFileDataAsync(FileMetadata file) => throw new NotImplementedException();
        protected override void ReadMetadata(BinaryReader r) => PakFormat02.Read(this, r);
        protected override void Write(BinaryWriter w) => PakFormat02.Write(this, w);
    }
}