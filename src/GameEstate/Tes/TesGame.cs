using System.ComponentModel;

namespace GameEstate.Tes
{
    /// <summary>
    /// TesGame
    /// </summary>
    public enum TesGame
    {
        // tes
        [Description("Morrowind")] Morrowind,
        [Description("Oblivion")] Oblivion,
        [Description("Skyrim")] Skyrim,
        [Description("Skyrim Special Edition")] SkyrimSE,
        [Description("Skyrim VR")] SkyrimVR,
        // fallout
        [Description("Fallout 3")] Fallout3,
        [Description("Fallout New Vegas")] FalloutNV,
        [Description("Fallout 4")] Fallout4,
        [Description("Fallout 4 VR")] Fallout4VR,
    }
}