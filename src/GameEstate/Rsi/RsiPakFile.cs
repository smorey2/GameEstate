using GameEstate.Core;
using GameEstate.Core.DataFormat;

namespace GameEstate.Rsi
{
    public class RsiPakFile : CorePakFile
    {
        static readonly byte[] Magic = { 0x50, 0x4B, 0x03, 0x14 };

        public RsiPakFile(string filePath) : base(filePath, new PakFormat01(Magic), new DatFormat01()) { }
    }
}