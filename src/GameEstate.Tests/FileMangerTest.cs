using GameEstate.Core;
using Xunit;

namespace GameEstate.CoreTests
{
    public class FileManagerTest
    {
        [Fact]
        public void CryEstate()
        {
            var fileManager = EstateManager.GetEstate("Cry").FileManager;
            //Assert.True(fileManager.IsDataPresent);
            //var abc0 = fileManager.GetGameFilePaths((int)CryGame.Unknown01, "Data.p4k");
            //Assert.Single(abc0);
        }

        [Fact]
        public void RedEstate()
        {
            var fileManager = EstateManager.GetEstate("Red").FileManager;
            Assert.True(fileManager.IsDataPresent);
            var abc1 = fileManager.GetGameFilePaths("Witcher", "2da00.bif");
            Assert.Single(abc1);
            var abc2 = fileManager.GetGameFilePaths("Witcher2", "tutorial.dzip");
            Assert.Single(abc2);
            var abc3 = fileManager.GetGameFilePaths("Witcher3", "metadata.store");
            Assert.Single(abc3);
        }

        [Fact]
        public void RsiEstate()
        {
            var fileManager = EstateManager.GetEstate("Rsi").FileManager;
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetGameFilePaths("StarCitizen", "Data.p4k");
            Assert.Single(abc0);
        }

        [Fact]
        public void TesEstate()
        {
            var fileManager = EstateManager.GetEstate("Tes").FileManager;
            Assert.True(fileManager.IsDataPresent);
            var abc0 = fileManager.GetGameFilePaths("Fallout4VR", "Fallout4 - *.ba2");
            Assert.Equal(21, abc0.Length);
            var abc1 = fileManager.GetGameFilePaths("Fallout4VR", "Fallout4 - Startup.ba2");
            Assert.Single(abc1);
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
