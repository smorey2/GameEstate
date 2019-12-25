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
            var fileManager = new CryFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetFilePaths(true, "Data.p4k", (int)CryGame.Unknown01);
            Assert.Single(abc0);
            var abc1 = fileManager.GetFilePaths(false, "Data.p4k", (int)CryGame.Unknown01);
            Assert.Single(abc1);
        }

        [Fact]
        public void RsiEstate()
        {
            var fileManager = new RsiFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetFilePaths(true, "Data.p4k", (int)RsiGame.StarCitizen);
            Assert.Single(abc0);
            var abc1 = fileManager.GetFilePaths(false, "Data.p4k", (int)RsiGame.StarCitizen);
            Assert.Single(abc1);
        }

        [Fact]
        public void TesEstate()
        {
            var fileManager = new TesFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetFilePaths(true, "Fallout4 - *.ba2", (int)TesGame.Fallout4VR);
            Assert.Equal(21, abc0.Length);
            var abc1 = fileManager.GetFilePaths(false, "Fallout4 - Startup.ba2", (int)TesGame.Fallout4VR);
            Assert.Single(abc1);
        }

        [Fact]
        public void U9Estate()
        {
            var fileManager = new U9FileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetFilePaths(true, "static/*.flx", (int)U9Game.UltimaIX);
            Assert.Equal(17, abc0.Length);
            var abc1 = fileManager.GetFilePaths(false, "static/activity.flx", (int)U9Game.UltimaIX);
            Assert.Single(abc1);
        }

        [Fact]
        public void UOEstate()
        {
            var fileManager = new UOFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetFilePaths(true, "*.idx", (int)UOGame.UltimaOnline);
            Assert.Equal(7, abc0.Length);
            var abc1 = fileManager.GetFilePaths(false, "anim.idx", (int)UOGame.UltimaOnline);
            Assert.Single(abc1);
        }
    }
}
