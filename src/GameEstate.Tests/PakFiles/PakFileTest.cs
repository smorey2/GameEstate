using System;
using Xunit;

namespace GameEstate.Tests.PakFiles
{
    public class PakFileTest
    {
        [Theory]
        [InlineData("game:/client_highres.dat#AC", "Texture060043BE", 32792)]
        public void ACEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("AC", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/game1.index#Dishonored2", "strings/english_m.lang", 765258)]
        public void ArkaneEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Arkane", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/GameData.pak#MechWarriorOnline", "GameModeObjects.xml", 153832)]
        public void CryEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Cry", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/Engine_Main_0.cpk#TheCouncil", "data/engine_0.prefab", 20704)]
        public void CyanideEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Cyanide", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/anim.idx#UltimaOnline", "Engine/default_cch.dds", 16520)]
        [InlineData("game:/static/activity.flx#UltimaIX", "Engine/default_cch.dds", 16520)]
        public void OriginEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Origin", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/Data.p4k#StarCitizen", "Engine/default_cch.dds", 16520)]
        public void RsiEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Rsi", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/main.key#Witcher", "2da00.bif", 887368)]
        [InlineData("game:/krbr.dzip#Witcher2", "globals/ch_credits_main.csv", 6716)]
        [InlineData("game:/content0/bundles/xml.bundle#Witcher3", "engine/physics/apexclothmaterialpresets.xml", 2512)]
        [InlineData("game:/content0/collision.cache#Witcher3", "engine/physics/apexclothmaterialpresets.xml", 2512)]
        [InlineData("game:/content0/dep.cache#Witcher3", "engine/physics/apexclothmaterialpresets.xml", 2512)]
        [InlineData("game:/content0/texture.cache#Witcher3", "environment/debug/debug-delete.xbm", 2512)]
        //[InlineData("game:/content0/texture.cache#Witcher3", "environment/skyboxes/textures/clouds_noise_m.xbm", 2512)]
        public void RedEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Red", uri, sampleFile, sampleFileSize);

        [Theory]
        [InlineData("game:/Morrowind.bsa#Morrowind", "textures/vfx_poison03.dds", 11040)]
        [InlineData("game:/Oblivion - Meshes.bsa#Oblivion", "trees/treecottonwoodsu.spt", 6329)]
        [InlineData("game:/Oblivion - Textures - Compressed.bsa#Oblivion", "textures/trees/canopyshadow.dds", 174904)]
        [InlineData("game:/Skyrim - Meshes0.bsa#SkyrimSE", "meshes/scalegizmo.nif", 8137)]
        [InlineData("game:/Skyrim - Textures0.bsa#SkyrimSE", "textures/actors/dog/dog.dds", 1398240)]
        [InlineData("game:/Fallout4 - Startup.ba2#Fallout4VR", "Textures/Water/WaterRainRipples.dds", 349680)]
        [InlineData("game:/Fallout4 - Textures8.ba2#Fallout4VR", "Textures/Terrain/DiamondCity/DiamondCity.16.-2.-2.DDS", 174904)]
        public void TesEstate(string uri, string sampleFile, int sampleFileSize) => EstateLoadFileData("Tes", uri, sampleFile, sampleFileSize);

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
