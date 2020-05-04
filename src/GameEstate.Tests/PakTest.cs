using GameEstate.Estates;
using GameEstate.Formats;
using Xunit;

namespace GameEstate.CoreTests
{
    public class PakTest
    {
        //[Theory]
        //[InlineData("Data.p4k", "Unknown01")]
        //public void CryEstate(string pakPath, string game)
        //{
        //    var fileManager =  EstateManager.GetEstate("Cry").FileManager;
        //    var path = fileManager.GetGameFilePaths((int)game, pakPath)[0];
        //    var pak = new CryPakFile(path, game);
        //}

        [Theory]
        [InlineData("Data.p4k", "StarCitizen")]
        public void RsiEstate(string pakPath, string game)
        {
            var fileManager = EstateManager.GetEstate("Rsi").FileManager;
            var path = fileManager.GetGameFilePaths(game, pakPath)[0];
            var pak = new RsiPakFile(path, game);
            //pak.ContainsFile("");
            //var abc = pak.LoadFileDataAsync("").Result;
        }

        [Theory]
        [InlineData("main.key", "Witcher")]
        [InlineData("krbr.dzip", "Witcher2")]
        [InlineData("Data.p4k", "Witcher3")]
        public void RedEstate(string pakPath, string game)
        {
            var fileManager = EstateManager.GetEstate("Red").FileManager;
            var path = fileManager.GetGameFilePaths(game, pakPath)[0];
            var pak = new RedPakFile(path, game);
            //pak.ContainsFile("");
            //var abc = pak.LoadFileDataAsync("").Result;
        }

        [Theory]
        [InlineData("Fallout4 - Startup.ba2", "Fallout4VR")]
        [InlineData("Fallout4 - Textures8.ba2", "Fallout4VR")]
        [InlineData("Oblivion - Meshes.bsa", "Oblivion")]
        [InlineData("Oblivion - Textures - Compressed.bsa", "Oblivion")]
        [InlineData("Morrowind.bsa", "Morrowind")]
        public void TesEstate(string pakPath, string game)
        {
            var fileManager = EstateManager.GetEstate("Tes").FileManager;
            var path = fileManager.GetGameFilePaths(game, pakPath)[0];
            var pak = new TesPakFile(path, game);
        }

        [Fact]
        public void U9Estate()
        {
            var fileManager = EstateManager.GetEstate("U9").FileManager;
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetGameFilePaths("UltimaIX", "static/*.flx");
            Assert.Equal(17, abc0.Length);
            var abc1 = fileManager.GetGameFilePaths("UltimaIX", "static/activity.flx");
            Assert.Single(abc1);
        }

        [Fact]
        public void UOEstate()
        {
            var fileManager = EstateManager.GetEstate("UO").FileManager;
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetGameFilePaths("UltimaOnline", "*.idx");
            Assert.Equal(7, abc0.Length);
            var abc1 = fileManager.GetGameFilePaths("UltimaOnline", "anim.idx");
            Assert.Single(abc1);
        }
    }
}
