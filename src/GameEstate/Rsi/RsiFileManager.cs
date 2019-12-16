using GameEstate.Core;

namespace GameEstate.Rsi
{
    public class RsiFileManager : CoreFileManager<RsiFileManager, RsiEstate, RsiGame>
    {
        protected override RsiFileManager Load()
        {
            _locations.Add(RsiGame.StarCitizen, @"D:\Roberts Space Industries\StarCitizen\LIVE");
            return this;
        }
    }
}