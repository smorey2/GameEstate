using GameEstate.Core;

namespace GameEstate.UO
{
    /// <summary>
    /// UOPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class UOPakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UOPakFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public UOPakFile(string filePath) : base(filePath, null, null) { }
    }
}