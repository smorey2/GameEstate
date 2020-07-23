using System;
using Xunit;

namespace GameEstate.CoreTests
{
    public class PakFileLoadAllTest
    {
        [Theory]
        [InlineData("game:/client_highres.dat#AC")]
        public void ACEstate(string uri) => EstateLoadAll("AC", uri);

        [Theory]
        [InlineData("game:/GameData.pak#MechWarriorOnline")]
        public void CryEstate(string uri) => EstateLoadAll("Cry", uri);

        [Theory]
        [InlineData("game:/Data.p4k#StarCitizen")]
        public void RsiEstate(string uri) => EstateLoadAll("Rsi", uri);

        [Theory]
        [InlineData("game:/main.key#Witcher")]
        [InlineData("game:/krbr.dzip#Witcher2")]
        [InlineData("game:/content0/bundles/xml.bundle#Witcher3")]
        [InlineData("game:/content0/collision.cache#Witcher3")]
        [InlineData("game:/content0/dep.cache#Witcher3")]
        public void RedEstate(string uri) => EstateLoadAll("Red", uri);

        [Theory]
        [InlineData("Morrowind.bsa#Morrowind")]
        [InlineData("Oblivion - Meshes.bsa#Oblivion")]
        [InlineData("Oblivion - Textures - Compressed.bsa#Oblivion")]
        [InlineData("Skyrim - Meshes0.bsa#SkyrimSE")]
        [InlineData("Skyrim - Textures0.bsa#SkyrimSE")]
        [InlineData("Fallout4 - Startup.ba2#Fallout4VR")]
        [InlineData("Fallout4 - Textures8.ba2#Fallout4VR")]
        public void TesEstate(string uri) => EstateLoadAll("Tes", uri);

        [Theory]
        [InlineData("game:/static/activity.flx#UltimaIX")]
        public void U9Estate(string uri) => EstateLoadAll("U9", uri);

        [Theory]
        [InlineData("game:/anim.idx#UltimaOnline")]
        public void UOEstate(string uri) => EstateLoadAll("UO", uri);

        [Theory]
        [InlineData("game:/core/pak01_dir.vpk#Dota2")]
        public void ValveEstate(string uri) => EstateLoadAll("Valve", uri);

        static void EstateLoadAll(string estate, string uri)
        {
            var pakFile = EstateManager.GetEstate(estate).OpenPakFile(new Uri(uri));
        }
    }
}
