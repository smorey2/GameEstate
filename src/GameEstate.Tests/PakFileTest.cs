using GameEstate.Estates;
using Xunit;

namespace GameEstate.CoreTests
{
    public class PakFileTest
    {
        [Theory]
        [InlineData("client_highres.dat", "AC", "Texture060043BE", 32792)]
        public void ACEstate(string pakPath, string game, string sampleFile, int sampleFileSize)
        {
            var fileManager = EstateManager.GetEstate("AC").FileManager;
            var path = fileManager.GetGameFilePaths(game, pakPath)[0];
            var pakFile = new ACPakFile(path, game);
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }

        [Theory]
        [InlineData("GameData.pak", "MechWarriorOnline", "GameModeObjects.xml", 153832)]
        public void CryEstate(string pakPath, string game, string sampleFile, int sampleFileSize)
        {
            var fileManager = EstateManager.GetEstate("Cry").FileManager;
            var path = fileManager.GetGameFilePaths(game, pakPath)[0];
            var pakFile = new CryPakFile(path, game);
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }

        [Theory]
        [InlineData("Data.p4k", "StarCitizen", "Engine/default_cch.dds", 16520)]
        public void RsiEstate(string pakPath, string game, string sampleFile, int sampleFileSize)
        {
            var fileManager = EstateManager.GetEstate("Rsi").FileManager;
            var path = fileManager.GetGameFilePaths(game, pakPath)[0];
            var pakFile = new RsiPakFile(path, game);
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }

        [Theory]
        [InlineData("main.key", "Witcher", "2da00.bif", 0)]
        [InlineData("krbr.dzip", "Witcher2", "globals/ch_credits_main.csv", 0)]
        //[InlineData("", "Witcher3", "", 0)]
        public void RedEstate(string pakPath, string game, string sampleFile, int sampleFileSize)
        {
            var fileManager = EstateManager.GetEstate("Red").FileManager;
            var path = fileManager.GetGameFilePaths(game, pakPath)[0];
            var pakFile = new RedPakFile(path, game);
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }

        [Theory]
        [InlineData("Fallout4 - Startup.ba2", "Fallout4VR", "Textures/Water/WaterRainRipples.dds", 0)]
        [InlineData("Fallout4 - Textures8.ba2", "Fallout4VR", "Textures/Terrain/DiamondCity/DiamondCity.16.-2.-2.DDS", 131072)]
        [InlineData("Oblivion - Meshes.bsa", "Oblivion", "trees/treecottonwoodsu.spt", 1333)]
        [InlineData("Oblivion - Textures - Compressed.bsa", "Oblivion", "textures/trees/canopyshadow.dds", 113732)]
        [InlineData("Morrowind.bsa", "Morrowind", "textures/vfx_poison03.dds", 11040)]
        public void TesEstate(string pakPath, string game, string sampleFile, int sampleFileSize)
        {
            var fileManager = EstateManager.GetEstate("Tes").FileManager;
            var path = fileManager.GetGameFilePaths(game, pakPath)[0];
            var pakFile = new TesPakFile(path, game);
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }

        [Theory]
        [InlineData("static/activity.flx", "UltimaIX", "Engine/default_cch.dds", 16520)]
        public void U9Estate(string pakPath, string game, string sampleFile, int sampleFileSize)
        {
            var fileManager = EstateManager.GetEstate("U9").FileManager;
            var path = fileManager.GetGameFilePaths(game, pakPath)[0];
            var pakFile = new U9PakFile(path, game);
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }

        [Theory]
        [InlineData("anim.idx", "UltimaOnline", "Engine/default_cch.dds", 16520)]
        public void UOEstate(string pakPath, string game, string sampleFile, int sampleFileSize)
        {
            var fileManager = EstateManager.GetEstate("UO").FileManager;
            var path = fileManager.GetGameFilePaths(game, pakPath)[0];
            var pakFile = new UOPakFile(path, game);
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }

        [Theory]
        [InlineData("core/pak01_dir.vpk", "Dota2", "stringtokendatabase.txt", 35624)]
        public void ValveEstate(string pakPath, string game, string sampleFile, int sampleFileSize)
        {
            var fileManager = EstateManager.GetEstate("Valve").FileManager;
            var path = fileManager.GetGameFilePaths(game, pakPath)[0];
            var pakFile = new ValvePakFile(path, game);
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }
    }
}
