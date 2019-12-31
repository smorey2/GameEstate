using GameEstate.Core;
using GameEstate.Formats.Binary;

namespace GameEstate.Tes
{
    /// <summary>
    /// TesPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class TesPakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TesPakFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public TesPakFile(string filePath) : base(filePath, new PakFormatTes(), new DatFormatTes()) => Open();
    }
}