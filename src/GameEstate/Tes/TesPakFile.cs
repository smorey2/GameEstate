using GameEstate.Core;
using GameEstate.Core.DataFormat;

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
        public TesPakFile(string filePath) : base(filePath, new PakFormat02(), new DatFormat02()) { }
    }
}