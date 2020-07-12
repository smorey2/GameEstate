using System;
using System.Collections.Generic;

namespace GameEstate
{
    public static class EstatePlatform
    {
        public const string PlatformWindows = "Windows";
        public const string PlatformUnknown = "";

        public static string Platform;

        public static readonly List<Func<bool>> Startups = new List<Func<bool>>();
    }
}