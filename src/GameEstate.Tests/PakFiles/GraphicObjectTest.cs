using System;
using System.Threading.Tasks;
using Xunit;

namespace GameEstate.Tests.PakFiles
{
    public class GraphicObjectTest
    {
        static GraphicObjectTest() => EstatePlatform.Startups.Add(TestPlatform.Startup);

        //[Theory]
        //[InlineData("game:/client_highres.dat#AC", "Texture060043BE")]
        //public void ACEstate(string uri, string sampleFile) => EstateLoadGraphicObject("AC", uri, sampleFile);

        //[Theory]
        //[InlineData("game:/GameData.pak#MechWarriorOnline", "GameModeObjects.xml")]
        //public void CryEstate(string uri, string sampleFile) => EstateLoadGraphicObject("Cry", uri, sampleFile);

        //[Theory]
        //[InlineData("game:/Data.p4k#StarCitizen", "Engine/default_cch.dds")]
        //public void RsiEstate(string uri, string sampleFile) => EstateLoadGraphicObject("Rsi", uri, sampleFile);

        //[Theory]
        //[InlineData("game:/main.key#Witcher", "2da00.bif")]
        //[InlineData("game:/krbr.dzip#Witcher2", "globals/ch_credits_main.csv")]
        //[InlineData("game:/content0/bundles/xml.bundle#Witcher3", "engine/physics/apexclothmaterialpresets.xml")]
        //[InlineData("game:/content0/collision.cache#Witcher3", "engine/physics/apexclothmaterialpresets.xml")]
        //[InlineData("game:/content0/dep.cache#Witcher3", "engine/physics/apexclothmaterialpresets.xml")]
        //public void RedEstate(string uri, string sampleFile) => EstateLoadGraphicObject("Red", uri, sampleFile);

        [Theory]
        [InlineData("game:/Morrowind.bsa#Morrowind", "meshes/x/ex_common_balcony_01.nif")]
        //[InlineData("game:/Oblivion - Meshes.bsa#Oblivion", "trees/treecottonwoodsu.spt")]
        //[InlineData("game:/Oblivion - Textures - Compressed.bsa#Oblivion", "textures/trees/canopyshadow.dds")]
        //[InlineData("game:/Skyrim - Meshes0.bsa#SkyrimSE", "meshes/scalegizmo.nif")]
        //[InlineData("game:/Skyrim - Textures0.bsa#SkyrimSE", "textures/actors/dog/dog.dds")]
        //[InlineData("game:/Fallout4 - Startup.ba2#Fallout4VR", "Textures/Water/WaterRainRipples.dds")]
        //[InlineData("game:/Fallout4 - Textures8.ba2#Fallout4VR", "Textures/Terrain/DiamondCity/DiamondCity.16.-2.-2.DDS")]
        public async Task TesEstate(string uri, string sampleFile) => await EstateLoadGraphicObject("Tes", uri, sampleFile);

        //[Theory]
        //[InlineData("game:/static/activity.flx#UltimaIX", "Engine/default_cch.dds")]
        //public void U9Estate(string uri, string sampleFile) => EstateLoadGraphicObject("U9", uri, sampleFile);

        //[Theory]
        //[InlineData("game:/anim.idx#UltimaOnline", "Engine/default_cch.dds")]
        //public void UOEstate(string uri, string sampleFile) => EstateLoadGraphicObject("UO", uri, sampleFile);

        //[Theory]
        //[InlineData("game:/core/pak01_dir.vpk#Dota2", "stringtokendatabase.txt")]
        //public void ValveEstate(string uri, string sampleFile) => EstateLoadGraphicObject("Valve", uri, sampleFile);

        async Task EstateLoadGraphicObject(string estate, string uri, string sampleFile)
        {
            if (sampleFile == null) return;
            var pakFile = EstateManager.GetEstate(estate).OpenPakFile(new Uri(uri));
            var obj0 = await pakFile.LoadFileObjectAsync<object>(sampleFile);
            //Assert.Equal(sampleFileSize, pakFile.GetLoadFileDataAsync(sampleFile).Result.Length);
        }
    }
}
