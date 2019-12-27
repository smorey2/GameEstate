using GameEstate.Cry;
using GameEstate.Red;
using GameEstate.Rsi;
using GameEstate.Tes;
using GameEstate.U9;
using GameEstate.UO;
using Xunit;

namespace GameEstate.CoreTests
{
    public class FileManagerTest
    {
        [Fact]
        public void CryEstate()
        {
            var fileManager = new CryFileManager();
            //Assert.True(fileManager.IsDataPresent);
            //var abc0 = fileManager.GetFilePaths("Data.p4k", (int)CryGame.Unknown01, true);
            //Assert.Single(abc0);
            //var abc1 = fileManager.GetFilePaths("Data.p4k", (int)CryGame.Unknown01, false);
            //Assert.Single(abc1);
        }

        [Fact]
        public void RKEstate()
        {
            var fileManager = new RedFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc1 = fileManager.GetFilePaths("2da00.bif", (int)RedGame.Witcher, true);
            Assert.Single(abc1);
            var abc2 = fileManager.GetFilePaths("tutorial.dzip", (int)RedGame.Witcher2, true);
            Assert.Single(abc2);
            var abc3 = fileManager.GetFilePaths("metadata.store", (int)RedGame.Witcher3, true);
            Assert.Single(abc3);
        }

        [Fact]
        public void RsiEstate()
        {
            var fileManager = new RsiFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetFilePaths("Data.p4k", (int)RsiGame.StarCitizen, true);
            Assert.Single(abc0);
            var abc1 = fileManager.GetFilePaths("Data.p4k", (int)RsiGame.StarCitizen, false);
            Assert.Single(abc1);
        }

        [Fact]
        public void TesEstate()
        {
            var fileManager = new TesFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetFilePaths("Fallout4 - *.ba2", (int)TesGame.Fallout4VR, true);
            Assert.Equal(21, abc0.Length);
            var abc1 = fileManager.GetFilePaths("Fallout4 - Startup.ba2", (int)TesGame.Fallout4VR, false);
            Assert.Single(abc1);
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
