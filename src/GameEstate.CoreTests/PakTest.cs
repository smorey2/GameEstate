using GameEstate.Cry;
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
        [InlineData("Data.p4k", CryGame.StarCitizen)]
        public void CryEstate(string pakPath, CryGame game)
        {
            var path = CryFileManager.GetFilePaths(false, pakPath, game)[0];
            var pak = new CryPakFile(path);
        }

        [Theory]
        [InlineData("Data.p4k", RsiGame.StarCitizen)]
        public void RsiEstate(string pakPath, RsiGame game)
        {
            var path = RsiFileManager.GetFilePaths(false, pakPath, game)[0];
            var pak = new RsiPakFile(path);
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
            var path = TesFileManager.GetFilePaths(false, pakPath, game)[0];
            var pak = new TesPakFile(path);
        }

        [Fact]
        public void U9Estate()
        {
            Assert.True(U9FileManager.IsDataPresent);
            var abc0 = U9FileManager.GetFilePaths(true, "static/*.flx", U9Game.UltimaIX);
            Assert.Equal(17, abc0.Length);
            var abc1 = U9FileManager.GetFilePaths(false, "static/activity.flx", U9Game.UltimaIX);
            Assert.Single(abc1);
        }

        [Fact]
        public void UOEstate()
        {
            Assert.True(UOFileManager.IsDataPresent);
            var abc0 = UOFileManager.GetFilePaths(true, "*.idx", UOGame.UltimaOnline);
            Assert.Equal(7, abc0.Length);
            var abc1 = UOFileManager.GetFilePaths(false, "anim.idx", UOGame.UltimaOnline);
            Assert.Single(abc1);
        }
    }
}
