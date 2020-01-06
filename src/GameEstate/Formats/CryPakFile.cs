using GameEstate.Core;

namespace GameEstate.Formats
{
    /// <summary>
    /// CryPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class CryPakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CryPakFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public CryPakFile(string filePath) : base(filePath, null) => Open();
    }
}