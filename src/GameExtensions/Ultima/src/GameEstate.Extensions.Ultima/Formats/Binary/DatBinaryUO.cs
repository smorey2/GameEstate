using GameEstate.Core;
using GameEstate.Formats.Binary.UltimaUO.Records;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary
{
    public class DatBinaryUO : DatBinary
    {
        public override async Task ReadAsync(BinaryDatFile source, BinaryReader r, ReadStage stage)
        {
            if (stage != ReadStage.File)
                throw new ArgumentOutOfRangeException(nameof(stage), stage.ToString());
            //if (string.Equals(source.Name, "tiledata.mul", StringComparison.OrdinalIgnoreCase))
            var tileData = new TileDataRecord();
            await tileData.ReadAsync(source);
            //return Task.CompletedTask;
        }
    }
}