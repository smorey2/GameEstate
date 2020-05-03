using GameEstate.Estates;
using GameEstate.Formats;
using Xunit;

namespace GameEstate.DataTests
{
    public class DatTest
    {
        [Theory]
        [InlineData(".", "UltimaOnline")]
        //[InlineData("tiledata.mul", "UltimaOnline")]
        public void UOEstate(string datPath, string game)
        {
            var fileManager = EstateManager.GetEstate("UO").FileManager;
            var path = fileManager.GetGameFilePaths(game, datPath)[0];
            using (var dat = new UODatFile(path, game))
            {

            }
        }
    }
}
