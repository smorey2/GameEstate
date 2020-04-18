using GameEstate.Modules;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace GameEstate.CoreTests.Modules
{
    public class QuickbmsTest
    {
        [Theory]
        [InlineData("Witcher3", @"D:\Program Files (x86)\GOG Galaxy\Games\The Witcher 3 Wild Hunt GOTY\content\*", "witcher3.bms")]
        public async Task ExtractPackageTest(string packageName, string contentPath, string scriptFile)
        {
            var packagePath = Path.Combine(Config.AssetPath, packageName);
            var scriptPath = Path.Combine(Config.BmsScriptPath, scriptFile);
            var success = await Quickbms.ExtractPackageAsync(packagePath, scriptPath, contentPath);
        }
    }
}
