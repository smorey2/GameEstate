using GameEstate.Formats.Collada;
using GameEstate.Formats.Cry;
using System.IO;
using Xunit;
using Xunit.Abstractions;
using static GameEstate.EstateDebug;

namespace GameEstate.Tests.Exports
{
    public class ColladaExportTests
    {
        static ColladaExportTests() => EstatePlatform.Startups.Add(TestPlatform.Startup);

        const string AssetRoot = @"D:\StarCitizen\data";

        public ColladaExportTests(ITestOutputHelper helper) => LogFunc = x => helper.WriteLine(x.ToString());

        [Theory]
        //[InlineData(@"Objects\animals\fish\CleanerFish_clean_prop_animal_01.chr")]
        [InlineData(@"Objects\buildingsets\human\hightech\prop\hydroponic\hydroponic_machine_1_incubator_01x01x02_a.cgf")]
        //[InlineData(@"Objects\buildingsets\human\hightech\prop\hydroponic\hydroponic_machine_1_incubator_02x01x012_a.cgf")]
        //[InlineData(@"Objects\buildingsets\human\hightech\prop\hydroponic\hydroponic_machine_1_incubator_rotary_025x01x0225_a.cga")]
        //[InlineData(@"Objects\buildingsets\human\hightech\prop\hydroponic\hydroponic_machine_1_incubator_rotary_025x01x0225_a.cgf")]
        public void LoadModel(string path)
        {
            var cryFile = new CryFile(Path.Combine(AssetRoot, path));
            cryFile.LoadFromFile();
            var daeFile = new ColladaObjectWriter(cryFile);
            daeFile.Write(@"C:\T_\Models", false);
        }
    }
}
