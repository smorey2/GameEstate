using GameEstate.Core;

namespace GameEstate.Red
{
    /// <summary>
    /// RedPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class RedPakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedPakFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public RedPakFile(string filePath) : base(filePath, null, null) { }
    }
}