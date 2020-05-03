using GameEstate.Core;
using GameEstate.Formats.Binary;
using System;
using System.Threading.Tasks;

namespace GameEstate.Estates
{
    public class ValvePakFile : AbstractPakFile
    {
        public ValvePakFile(string game, string name) : base(game, name)
        {
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override bool Contains(string filePath)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

        public override Task<byte[]> LoadFileDataAsync(string filePath, Action<FileMetadata, string> exception)
        {
            throw new NotImplementedException();
        }
    }
}