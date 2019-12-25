using GameEstate.Core;

namespace GameEstate.U9
{
    public class U9FileManager : CoreFileManager<U9FileManager, U9Game>
    {
        protected override U9FileManager Load()
        {
            LoadFromRegKeys(false, new object[] {
                @"GOG.com\GOGULTIMA9", U9Game.UltimaIX,
                @"GOG.com\Games\1207659093", U9Game.UltimaIX,
            });
            return this;
        }
    }
}