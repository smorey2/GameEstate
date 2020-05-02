using GameEstate.Core;

namespace GameEstate.Estates
{
    /// <summary>
    /// CryPakFile
    /// </summary>
    /// <seealso cref="GameEstate.Core.BinaryPakFile" />
    public class CryPakFile : BinaryPakFile
    {
        /// <summary>Initializes a new instance of the <see cref="CryPakFile" /> class.</summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="game"></param>
        public CryPakFile(string filePath, string game) : base(filePath, game, null) => Open();
    }
}