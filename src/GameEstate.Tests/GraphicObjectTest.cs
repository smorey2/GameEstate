using GameEstate.Graphics;
using System;
using Xunit;

namespace GameEstate.CoreTests
{
    public class GraphicObjectTest
    {
        [Theory]
        [InlineData("game:/client_highres.dat#AC", "Texture060043BE")]
        public void ACEstate(string uri, string sampleFile) => EstateLoadGraphicObject("AC", uri, sampleFile);

        [Theory]
        [InlineData("game:/GameData.pak#MechWarriorOnline", "GameModeObjects.xml")]
        public void CryEstate(string uri, string sampleFile) => EstateLoadGraphicObject("Cry", uri, sampleFile);

        [Theory]
        [InlineData("game:/Data.p4k#StarCitizen", "Engine/default_cch.dds")]
        public void RsiEstate(string uri, string sampleFile) => EstateLoadGraphicObject("Rsi", uri, sampleFile);

        [Theory]
        [InlineData("game:/main.key#Witcher", "2da00.bif")]
        [InlineData("game:/krbr.dzip#Witcher2", "globals/ch_credits_main.csv")]
        [InlineData("game:/content0/bundles/xml.bundle#Witcher3", "engine/physics/apexclothmaterialpresets.xml")]
        [InlineData("game:/content0/collision.cache#Witcher3", "engine/physics/apexclothmaterialpresets.xml")]
        [InlineData("game:/content0/dep.cache#Witcher3", "engine/physics/apexclothmaterialpresets.xml")]
        public void RedEstate(string uri, string sampleFile) => EstateLoadGraphicObject("Red", uri, sampleFile);

        [Theory]
        [InlineData("Morrowind.bsa#Morrowind", "textures/vfx_poison03.dds")]
        [InlineData("Oblivion - Meshes.bsa#Oblivion", "trees/treecottonwoodsu.spt")]
        [InlineData("Oblivion - Textures - Compressed.bsa#Oblivion", "textures/trees/canopyshadow.dds")]
        [InlineData("Skyrim - Meshes0.bsa#SkyrimSE", "meshes/scalegizmo.nif")]
        [InlineData("Skyrim - Textures0.bsa#SkyrimSE", "textures/actors/dog/dog.dds")]
        [InlineData("Fallout4 - Startup.ba2#Fallout4VR", "Textures/Water/WaterRainRipples.dds")]
        [InlineData("Fallout4 - Textures8.ba2#Fallout4VR", "Textures/Terrain/DiamondCity/DiamondCity.16.-2.-2.DDS")]
        public void TesEstate(string uri, string sampleFile) => EstateLoadGraphicObject("Tes", uri, sampleFile);

        [Theory]
        [InlineData("game:/static/activity.flx#UltimaIX", "Engine/default_cch.dds")]
        public void U9Estate(string uri, string sampleFile) => EstateLoadGraphicObject("U9", uri, sampleFile);

        [Theory]
        [InlineData("game:/anim.idx#UltimaOnline", "Engine/default_cch.dds")]
        public void UOEstate(string uri, string sampleFile) => EstateLoadGraphicObject("UO", uri, sampleFile);

        [Theory]
        [InlineData("game:/core/pak01_dir.vpk#Dota2", "stringtokendatabase.txt")]
        public void ValveEstate(string uri, string sampleFile) => EstateLoadGraphicObject("Valve", uri, sampleFile);

        void EstateLoadGraphicObject(string estate, string uri, string sampleFile)
        {
            if (sampleFile == null) return;
            var pakFile = EstateManager.GetEstate(estate).OpenPakFile(new Uri(uri));
            Assert.True(pakFile.Contains(sampleFile));
            //Assert.Equal(sampleFileSize, pakFile.GetLoadFileDataAsync(sampleFile).Result.Length);
        }
    }
}
