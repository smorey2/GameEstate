using GameEstate.Core;

namespace GameEstate.Bms
{
    /// <summary>
    /// BmsPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.CorePakFile" />
    public class BmsPakFile : CorePakFile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BmsPakFile"/> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        public BmsPakFile(string filePath) : base(filePath, null) => Open();
    }
}