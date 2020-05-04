using GameEstate.Core;
using System.Threading.Tasks;

namespace GameEstate.Formats.Binary.UltimaUO.Records
{
    public class MultiDataRecord
    {
        public MultiComponentList[] Components;

        public Task ReadAsync(BinaryDatFile source, TileDataRecord tileData)
        {
            var idx = source.GetReader("multi.idx");
            var mul = source.GetReader("multi.mul");
            Components = new MultiComponentList[(int)(idx.BaseStream.Length / 12)];
            using (var vd = source.GetReader("verdata.mul"))
                if (vd != null)
                    for (var i = 0; i < vd.ReadInt32(); ++i)
                    {
                        var file = vd.ReadInt32();
                        var index = vd.ReadInt32();
                        var lookup = vd.ReadInt32();
                        var length = vd.ReadInt32();
                        var extra = vd.ReadInt32();
                        if (file == 14 && index >= 0 && index < Components.Length && lookup >= 0 && length > 0)
                        {
                            vd.Position(lookup);
                            Components[index] = new MultiComponentList(tileData, vd, length / 12);
                            vd.Position(24 + (i * 20));
                        }
                    }
            return Task.CompletedTask;
        }
    }
}