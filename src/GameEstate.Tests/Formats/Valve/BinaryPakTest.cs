using GameEstate.Formats.Valve;
using System;
using Xunit;

namespace GameEstate.Tests.Formats.Valve
{
    public class BinaryPakTest
    {
        static BinaryPakTest() => EstatePlatform.Startups.Add(TestPlatform.Startup);

        [Theory]
        [InlineData("game:/dota/pak01_dir.vpk#Dota2", "materials/models/courier/frog/frog_color_psd_15017e0b.vtex_c")]
        [InlineData("game:/dota/pak01_dir.vpk#Dota2", "materials/models/courier/frog/frog_normal_psd_a5b783cb.vtex_c")]
        [InlineData("game:/dota/pak01_dir.vpk#Dota2", "materials/models/courier/frog/frog_specmask_tga_a889a311.vtex_c")]
        public void AGRP(string uri, string sampleFile) => EstateLoadFileObject<BinaryPak>("Valve", uri, sampleFile);

        [Theory]
        [InlineData("game:/dota/pak01_dir.vpk#Dota2",  "materials/models/courier/frog/frog_color_psd_15017e0b.vtex_c")]
        [InlineData("game:/dota/pak01_dir.vpk#Dota2", "materials/models/courier/frog/frog_normal_psd_a5b783cb.vtex_c")]
        [InlineData("game:/dota/pak01_dir.vpk#Dota2", "materials/models/courier/frog/frog_specmask_tga_a889a311.vtex_c")]
        public void DATATexture(string uri, string sampleFile) => EstateLoadFileObject<BinaryPak>("Valve", uri, sampleFile);

        [Theory]
        [InlineData("game:/dota/pak01_dir.vpk#Dota2", "materials/models/courier/frog/frog.vmat_c")]
        [InlineData("game:/dota/pak01_dir.vpk#Dota2", "materials/vgui/800corner.vmat_c")]
        public void DATAMaterial(string uri, string sampleFile) => EstateLoadFileObject<BinaryPak>("Valve", uri, sampleFile);

        static void EstateLoadFileObject<T>(string estate, string uri, string sampleFile)
        {
            if (sampleFile == null) return;
            var pakFile = EstateManager.GetEstate(estate).OpenPakFile(new Uri(uri));
            Assert.True(pakFile.Contains(sampleFile));
            var result = pakFile.LoadFileObjectAsync<T>(sampleFile).Result;
        }
    }
}
