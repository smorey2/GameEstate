using GameEstate.Core;
using System.IO;

namespace GameEstate.Tes
{
    /// <summary>
    /// TesFileManager
    /// </summary>
    /// <seealso cref="GameEstate.Core.CoreFileManager" />
    public class TesFileManager : CoreFileManager
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TesFileManager"/> class.
        /// </summary>
        public TesFileManager()
        {
            LoadFromRegKeys(new object[] {
                @"Bethesda Softworks\Oblivion", TesGame.Oblivion,
                @"Bethesda Softworks\Skyrim", TesGame.Skyrim,
                @"Bethesda Softworks\Fallout 3", TesGame.Fallout3,
                @"Bethesda Softworks\Fallout NV", TesGame.FalloutNV,
                @"Bethesda Softworks\Morrowind", TesGame.Morrowind,
                @"Bethesda Softworks\Fallout 4", TesGame.Fallout4,
                @"Bethesda Softworks\Skyrim SE", TesGame.SkyrimSE,
                @"Bethesda Softworks\Fallout 4 VR", TesGame.Fallout4VR,
                @"Bethesda Softworks\Skyrim VR", TesGame.SkyrimVR
            }, game => "Data", true);
            // hard-add
            var morrowind = @"C:\Program Files (x86)\Steam\steamapps\common\Morrowind";
            if (Directory.Exists(morrowind))
            {
                var dataPath = Path.Combine(morrowind, "Data Files");
                Locations.Add((int)TesGame.Morrowind, dataPath);
            }
        }
    }
}