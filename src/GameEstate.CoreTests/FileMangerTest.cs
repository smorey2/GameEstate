using GameEstate.Cry;
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
            Assert.True(CryFileManager.IsDataPresent);
            var abc0 = CryFileManager.GetFilePaths(true, "Data.p4k", CryGame.StarCitizen);
            Assert.Single(abc0);
            var abc1 = CryFileManager.GetFilePaths(false, "Data.p4k", CryGame.StarCitizen);
            Assert.Single(abc1);
        }

        [Fact]
        public void RsiEstate()
        {
            Assert.True(RsiFileManager.IsDataPresent);
            var abc0 = RsiFileManager.GetFilePaths(true, "Data.p4k", RsiGame.StarCitizen);
            Assert.Single(abc0);
            var abc1 = RsiFileManager.GetFilePaths(false, "Data.p4k", RsiGame.StarCitizen);
            Assert.Single(abc1);
        }

        [Fact]
        public void TesEstate()
        {
            Assert.True(TesFileManager.IsDataPresent);
            var abc0 = TesFileManager.GetFilePaths(true, "Fallout4 - *.ba2", TesGame.Fallout4VR);
            Assert.Equal(21, abc0.Length);
            var abc1 = TesFileManager.GetFilePaths(false, "Fallout4 - Startup.ba2", TesGame.Fallout4VR);
            Assert.Single(abc1);
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
