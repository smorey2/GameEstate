using System.Globalization;

namespace GameEstate.Core
{
    public static class Utility
    {
        public static bool TryParseInt32(string s, out int result) => !s.StartsWith("0x") ? int.TryParse(s, out result) : int.TryParse(s.Substring(2), NumberStyles.HexNumber, null, out result);
    }
}