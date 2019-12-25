using GameEstate.Core;

namespace GameEstate.UO
{
    public class UOFileManager : CoreFileManager<UOFileManager, UOGame>
    {
        protected override UOFileManager Load()
        {
            LoadFromRegKeys(false, new object[] {
                @"Origin Worlds Online\Ultima Online\KR Legacy Beta", UOGame.UltimaOnline,
                @"EA Games\Ultima Online: Mondain's Legacy\1.00.0000", UOGame.UltimaOnline,
                @"Origin Worlds Online\Ultima Online\1.0", UOGame.UltimaOnline,
                @"Origin Worlds Online\Ultima Online Third Dawn\1.0", UOGame.UltimaOnline,
                @"EA GAMES\Ultima Online Samurai Empire", UOGame.UltimaOnline,
                @"EA Games\Ultima Online: Mondain's Legacy", UOGame.UltimaOnline,
                @"EA GAMES\Ultima Online Samurai Empire\1.0", UOGame.UltimaOnline,
                @"EA GAMES\Ultima Online Samurai Empire\1.00.0000", UOGame.UltimaOnline,
                @"EA GAMES\Ultima Online: Samurai Empire\1.0", UOGame.UltimaOnline,
                @"EA GAMES\Ultima Online: Samurai Empire\1.00.0000", UOGame.UltimaOnline,
                @"EA Games\Ultima Online: Mondain's Legacy\1.0", UOGame.UltimaOnline,
                @"EA Games\Ultima Online: Mondain's Legacy\1.00.0000", UOGame.UltimaOnline,
                @"Origin Worlds Online\Ultima Online Samurai Empire BETA\2d\1.0", UOGame.UltimaOnline,
                @"Origin Worlds Online\Ultima Online Samurai Empire BETA\3d\1.0", UOGame.UltimaOnline,
                @"Origin Worlds Online\Ultima Online Samurai Empire\2d\1.0", UOGame.UltimaOnline,
                @"Origin Worlds Online\Ultima Online Samurai Empire\3d\1.0", UOGame.UltimaOnline,
                @"Electronic Arts\EA Games\Ultima Online Stygian Abyss Classic", UOGame.UltimaOnline,
                @"Electronic Arts\EA Games\Ultima Online Classic", UOGame.UltimaOnline,
                @"Electronic Arts\EA Games\", UOGame.UltimaOnline
            });
            return this;
        }
    }
}