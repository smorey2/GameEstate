using GameEstate.Formats.Binary;

namespace GameEstate.Core
{
    /// <summary>
    /// FilePakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class FilePakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilePakFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="tag">The tag.</param>
        public FilePakFile(string filePath) : base(filePath, new PakFormatFile(), new DatFormatFile()) { }
    }
}