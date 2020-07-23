using System;
using Xunit;

namespace GameEstate.CoreTests
{
    public class PakFileTest
    {
        [Theory]
        [InlineData("game:/client_highres.dat#AC", "Texture060043BE", 32792)]
        public void ACEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("AC", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/GameData.pak#MechWarriorOnline", "GameModeObjects.xml", 153832)]
        public void CryEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Cry", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/Data.p4k#StarCitizen", "Engine/default_cch.dds", 16520)]
        public void RsiEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Rsi", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/main.key#Witcher", "2da00.bif", 887368)]
        [InlineData("game:/krbr.dzip#Witcher2", "globals/ch_credits_main.csv", 6716)]
        [InlineData("game:/content0/bundles/xml.bundle#Witcher3", "engine/physics/apexclothmaterialpresets.xml", 2512)]
        [InlineData("game:/content0/collision.cache#Witcher3", "engine/physics/apexclothmaterialpresets.xml", 2512)]
        [InlineData("game:/content0/dep.cache#Witcher3", "engine/physics/apexclothmaterialpresets.xml", 2512)]
        public void RedEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Red", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("Morrowind.bsa#Morrowind", "textures/vfx_poison03.dds", 11040)]
        [InlineData("Oblivion - Meshes.bsa#Oblivion", "trees/treecottonwoodsu.spt", 1333)]
        [InlineData("Oblivion - Textures - Compressed.bsa#Oblivion", "textures/trees/canopyshadow.dds", 113732)]
        [InlineData("Skyrim - Meshes0.bsa#SkyrimSE", "meshes/scalegizmo.nif", 113732)]
        [InlineData("Skyrim - Textures0.bsa#SkyrimSE", "textures/actors/dog/dog.dds", 113732)]
        [InlineData("Fallout4 - Startup.ba2#Fallout4VR", "Textures/Water/WaterRainRipples.dds", 0)]
        [InlineData("Fallout4 - Textures8.ba2#Fallout4VR", "Textures/Terrain/DiamondCity/DiamondCity.16.-2.-2.DDS", 131072)]
        public void TesEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Tes", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/static/activity.flx#UltimaIX", "Engine/default_cch.dds", 16520)]
        public void U9Estate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("U9", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/anim.idx#UltimaOnline", "Engine/default_cch.dds", 16520)]
        public void UOEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("UO", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/core/pak01_dir.vpk#Dota2",  "stringtokendatabase.txt", 35624)]
        public void ValveEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Valve", uri, sampleFile, sampleFileSize);

        static void EstateLoadFileData(string estate, string uri, string sampleFile, int sampleFileSize)
        {
            if (sampleFile == null) return;
            var pakFile = EstateManager.GetEstate(estate).OpenPakFile(new Uri(uri));
            Assert.True(pakFile.Contains(sampleFile));
            Assert.Equal(sampleFileSize, pakFile.LoadFileDataAsync(sampleFile).Result.Length);
        }
    }
}
