using GameEstate.Core;
using GameEstate.Formats.Binary;

namespace GameEstate.Formats
{
    /// <summary>
    /// RsiPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class RsiPakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RsiPakFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public RsiPakFile(string filePath) : base(filePath, new PakFormatP4k()) => Open();
    }
}