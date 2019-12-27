using GameEstate.Cry;
using GameEstate.Red;
using GameEstate.Rsi;
using GameEstate.Tes;
using GameEstate.U9;
using GameEstate.UO;
using Xunit;

namespace GameEstate.CoreTests
{
    public class PakTest
    {
        [Theory]
        [InlineData("Data.p4k", CryGame.Unknown01)]
        public void CryEstate(string pakPath, CryGame game)
        {
            var fileManager = new CryFileManager();
            var path = fileManager.GetFilePaths(pakPath, (int)game, false)[0];
            var pak = new CryPakFile(path);
        }

        [Theory]
        [InlineData("Data.p4k", RsiGame.StarCitizen)]
        public void RsiEstate(string pakPath, RsiGame game)
        {
            var fileManager = new RsiFileManager();
            var path = fileManager.GetFilePaths(pakPath, (int)game, false)[0];
            var pak = new RsiPakFile(path);
            //pak.ContainsFile("");
            //var abc = pak.LoadFileDataAsync("").Result;
        }

        [Theory]
        [InlineData("Data.p4k", RedGame.Witcher)]
        [InlineData("Data.p4k", RedGame.Witcher2)]
        [InlineData("Data.p4k", RedGame.Witcher3)]
        public void RKEstate(string pakPath, RedGame game)
        {
            var fileManager = new RedFileManager();
            var path = fileManager.GetFilePaths(pakPath, (int)game, false)[0];
            var pak = new RedPakFile(path);
            //pak.ContainsFile("");
            //var abc = pak.LoadFileDataAsync("").Result;
        }

        [Theory]
        [InlineData("Fallout4 - Startup.ba2", TesGame.Fallout4VR)]
        [InlineData("Fallout4 - Textures8.ba2", TesGame.Fallout4VR)]
        [InlineData("Oblivion - Meshes.bsa", TesGame.Oblivion)]
        [InlineData("Oblivion - Textures - Compressed.bsa", TesGame.Oblivion)]
        [InlineData("Morrowind.bsa", TesGame.Morrowind)]
        public void TesEstate(string pakPath, TesGame game)
        {
            var fileManager = new TesFileManager();
            var path = fileManager.GetFilePaths(pakPath, (int)game, false)[0];
            var pak = new TesPakFile(path);
        }

        [Fact]
        public void U9Estate()
        {
            var fileManager = new U9FileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetFilePaths("static/*.flx", (int)U9Game.UltimaIX, true);
            Assert.Equal(17, abc0.Length);
            var abc1 = fileManager.GetFilePaths("static/activity.flx", (int)U9Game.UltimaIX, false);
            Assert.Single(abc1);
        }

        [Fact]
        public void UOEstate()
        {
            var fileManager = new UOFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetFilePaths("*.idx", (int)UOGame.UltimaOnline, true);
            Assert.Equal(7, abc0.Length);
            var abc1 = fileManager.GetFilePaths("anim.idx", (int)UOGame.UltimaOnline, false);
            Assert.Single(abc1);
        }
    }
}
