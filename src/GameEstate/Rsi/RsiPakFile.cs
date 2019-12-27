using GameEstate.Core;
using GameEstate.Formats.Binary;

namespace GameEstate.Rsi
{
    /// <summary>
    /// RsiPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class RsiPakFile : CorePakFile
    {
        static readonly byte[] Magic = { 0x50, 0x4B, 0x03, 0x14 };

        /// <summary>
        /// Initializes a new instance of the <see cref="RsiPakFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public RsiPakFile(string filePath) : base(filePath, new PakFormatP4k(Magic), new DatFormatPk4()) { }
    }
}