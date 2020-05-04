using GameEstate.Formats;
using Xunit;

namespace GameEstate.Extensions.Tes.Tests
{
    public class DatTest
    {
        [Theory]
        [InlineData("Morrowind.esm", "Morrowind")]
        //[InlineData("Bloodmoon.esm", "Morrowind")]
        //[InlineData("Tribunal.esm", "Morrowind")]
        //[InlineData("Oblivion.esm", "Oblivion")]
        //[InlineData("Skyrim.esm", "SkyrimVR")]
        //[InlineData("Fallout4.esm", "Fallout4")]
        //[InlineData("Fallout4.esm", "Fallout4VR")]
        public void TesEstate(string datPath, string game)
        {
            var fileManager = EstateManager.GetEstate("Tes").FileManager;
            var path = fileManager.GetGameFilePaths(game, datPath)[0];
            using (var dat = new TesDatFile(path, game))
            {

            }
        }
    }
}
