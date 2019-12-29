using GameEstate.Core;

namespace GameEstate.Rsi
{
    /// <summary>
    /// RsiFileManager
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreFileManager" />
    public class RsiFileManager : CoreFileManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RsiFileManager"/> class.
        /// </summary>
        public RsiFileManager() => Locations.Add((int)RsiGame.StarCitizen, @"D:\Roberts Space Industries\StarCitizen\LIVE");
    }
}