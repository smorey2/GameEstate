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
            //var abc0 = fileManager.GetGameFilePaths((int)CryGame.Unknown01, "Data.p4k");
            //Assert.Single(abc0);
        }

        [Fact]
        public void RedEstate()
        {
            var fileManager = new RedFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc1 = fileManager.GetGameFilePaths((int)RedGame.Witcher, "2da00.bif");
            Assert.Single(abc1);
            var abc2 = fileManager.GetGameFilePaths((int)RedGame.Witcher2, "tutorial.dzip");
            Assert.Single(abc2);
            var abc3 = fileManager.GetGameFilePaths((int)RedGame.Witcher3, "metadata.store");
            Assert.Single(abc3);
        }

        [Fact]
        public void RsiEstate()
        {
            var fileManager = new RsiFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetGameFilePaths((int)RsiGame.StarCitizen, "Data.p4k");
            Assert.Single(abc0);
        }

        [Fact]
        public void TesEstate()
        {
            var fileManager = new TesFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetGameFilePaths((int)TesGame.Fallout4VR, "Fallout4 - *.ba2");
            Assert.Equal(21, abc0.Length);
            var abc1 = fileManager.GetGameFilePaths((int)TesGame.Fallout4VR, "Fallout4 - Startup.ba2");
            Assert.Single(abc1);
        }

        [Fact]
        public void U9Estate()
        {
            var fileManager = new U9FileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetGameFilePaths((int)U9Game.UltimaIX, "static/*.flx");
            Assert.Equal(17, abc0.Length);
            var abc1 = fileManager.GetGameFilePaths((int)U9Game.UltimaIX, "static/activity.flx");
            Assert.Single(abc1);
        }

        [Fact]
        public void UOEstate()
        {
            var fileManager = new UOFileManager();
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetGameFilePaths((int)UOGame.UltimaOnline, "*.idx");
            Assert.Equal(7, abc0.Length);
            var abc1 = fileManager.GetGameFilePaths((int)UOGame.UltimaOnline, "anim.idx");
            Assert.Single(abc1);
        }
    }
}
