using GameEstate.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class DatBinaryStream : DatBinary
    {
        public override Task ReadAsync(BinaryDatFile source, BinaryReader r, ReadStage stage)
        {
            switch (stage)
            {
                default: throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());
            }
        }

        public override Task WriteAsync(BinaryDatFile source, BinaryWriter w, WriteStage stage)
        {
            switch (stage)
            {
                default: throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());
            }
        }
    }
}