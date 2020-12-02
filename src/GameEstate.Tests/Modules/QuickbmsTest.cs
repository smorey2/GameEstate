//using GameEstate.Modules;
//using System.IO;
//using System.Threading.Tasks;
//using Xunit;

//namespace GameEstate.CoreTests.Modules
//{
//    public class QuickbmsTest
//    {
//        static QuickbmsTest() => EstatePlatform.Startups.Add(TestPlatform.Startup);

//        [Theory]
//        [InlineData("Witcher3", @"D:\Program Files (x86)\GOG Galaxy\Games\The Witcher 3 Wild Hunt GOTY\content\*", "witcher3.bms")]
//        [InlineData("DungeonKeeper2", @"D:\Program Files (x86)\GOG Galaxy\Games\Dungeon Keeper 2\Data\*", "dk2.bms")]
//        [InlineData("Fallout", @"D:\Program Files (x86)\GOG Galaxy\Games\Fallout\*.dat", "fallout1.bms")]
//        [InlineData("Fallout2", @"D:\Program Files (x86)\GOG Galaxy\Games\Fallout 2\*.dat", "fallout2.bms")]
//        public async Task ExtractPackageTest(string packageName, string contentPath, string scriptFile)
//        {
//            var packagePath = Path.Combine(Config.AssetPath, packageName);
//            var scriptPath = Path.Combine(Config.BmsScriptPath, scriptFile);
//            var returnCode = await Quickbms.ExtractPackageAsync(packagePath, scriptPath, contentPath);
//            Assert.True(returnCode == 0);
//        }
//    }
//}
