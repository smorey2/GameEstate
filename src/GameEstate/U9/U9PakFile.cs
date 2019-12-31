using GameEstate.Core;

namespace GameEstate.U9
{
    /// <summary>
    /// U9PakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class U9PakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="U9PakFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public U9PakFile(string filePath) : base(filePath, null, null) => Open();
    }
}