using GameEstate.Core;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class DatFormatStream : DatFormat
    {
        public override Task ReadAsync(CoreDatFile source, BinaryReader r, ReadStage stage)
        {
            switch (stage)
            {
                default: throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());
            }
        }

        public override Task WriteAsync(CoreDatFile source, BinaryWriter w, WriteStage stage)
        {
            switch (stage)
            {
                default: throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());
            }
        }
    }
}