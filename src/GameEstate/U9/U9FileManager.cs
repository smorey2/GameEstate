using GameEstate.Core;

namespace GameEstate.U9
{
    /// <summary>
    /// U9FileManager
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreFileManager" />
    public class U9FileManager : CoreFileManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="U9FileManager"/> class.
        /// </summary>
        public U9FileManager() => LoadFromRegKeys(false, new object[] {
            @"GOG.com\GOGULTIMA9", U9Game.UltimaIX,
            @"GOG.com\Games\1207659093", U9Game.UltimaIX,
        });
    }
}