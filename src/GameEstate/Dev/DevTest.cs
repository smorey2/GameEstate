using GameEstate.Modules;
using System.IO;
using System.Threading.Tasks;

namespace GameEstate.Dev
{
    public class DevTest
    {
        public async Task TestAsync()
        {
            var returnCode = 0;
            //returnCode |= await ExtractPackageTest("Witcher3", @"D:\Program Files (x86)\GOG Galaxy\Games\The Witcher 3 Wild Hunt GOTY\content\*", "witcher3.bms");
            //returnCode |= await ExtractPackageTest("DungeonKeeper2", @"D:\Program Files (x86)\GOG Galaxy\Games\Dungeon Keeper 2\Data\*", "dk2.bms");
            //returnCode |= await ExtractPackageTest("Fallout", @"D:\Program Files (x86)\GOG Galaxy\Games\Fallout\*.dat", "fallout1.bms");
            returnCode |= await ExtractPackageTest("Fallout2", @"D:\Program Files (x86)\GOG Galaxy\Games\Fallout 2\*.dat", "fallout2.bms");
        }

        public async Task<int> ExtractPackageTest(string packageName, string contentPath, string scriptFile)
        {
            var packagePath = Path.Combine(Config.AssetPath, packageName);
            var scriptPath = Path.Combine(Config.BmsScriptPath, scriptFile);
            return await Quickbms.ExtractPackageAsync(packagePath, scriptPath, contentPath);
        }
    }
}