using GameEstate.Core;

namespace GameEstate.Red
{
    // Project RED
    // REDKit
    // https://witcher.fandom.com/wiki/File_format
    // http://jlouisb.users.sourceforge.net/

    /// <summary>
    /// RedFileManager
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreFileManager" />
    public class RedFileManager : CoreFileManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UOFileManager"/> class.
        /// </summary>
        public RedFileManager() => LoadFromRegKeys(new object[] {
            @"GOG.com\Games\1207658924", RedGame.Witcher,
            @"GOG.com\Games\1207658930", RedGame.Witcher2,
            @"GOG.com\Games\1495134320", RedGame.Witcher3,
        }, game => game == (int)RedGame.Witcher ? "Data"
            : game == (int)RedGame.Witcher2 ? "CookedPC"
            : game == (int)RedGame.Witcher3 ? "content"
            : null
        );
    }
}