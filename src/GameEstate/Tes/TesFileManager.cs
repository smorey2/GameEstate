using GameEstate.Core;
using System.IO;

namespace GameEstate.Tes
{
    public class TesFileManager : CoreFileManager<TesFileManager, TesGame>
    {
        protected override TesFileManager Load()
        {
            LoadFromRegKeys(true, new object[] {
                @"Bethesda Softworks\Oblivion", TesGame.Oblivion,
                @"Bethesda Softworks\Skyrim", TesGame.Skyrim,
                @"Bethesda Softworks\Fallout 3", TesGame.Fallout3,
                @"Bethesda Softworks\Fallout NV", TesGame.FalloutNV,
                @"Bethesda Softworks\Morrowind", TesGame.Morrowind,
                @"Bethesda Softworks\Fallout 4", TesGame.Fallout4,
                @"Bethesda Softworks\Skyrim SE", TesGame.SkyrimSE,
                @"Bethesda Softworks\Fallout 4 VR", TesGame.Fallout4VR,
                @"Bethesda Softworks\Skyrim VR", TesGame.SkyrimVR
            }, "Data");
            // hard-add
            var morrowind = @"C:\Program Files (x86)\Steam\steamapps\common\Morrowind";
            if (Directory.Exists(morrowind))
            {
                var dataPath = Path.Combine(morrowind, "Data Files");
                _locations.Add(TesGame.Morrowind, dataPath);
            }
            return this;
        }
    }
}