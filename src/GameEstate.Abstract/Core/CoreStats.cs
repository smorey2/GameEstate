using System;
using System.Diagnostics;

namespace GameEstate.Core
{
    public class CoreStats
    {
        /* 
        * DateTime.Now and DateTime.UtcNow are based on actual system clock time.
        * The resolution is acceptable but large clock jumps are possible and cause issues.
        * GetTickCount and GetTickCount64 have poor resolution.
        * GetTickCount64 is unavailable on Windows XP and Windows Server 2003.
        * Stopwatch.GetTimestamp() (QueryPerformanceCounter) is high resolution, but
        * somewhat expensive to call because of its defference to DateTime.Now,
        * which is why Stopwatch has been used to verify HRT before calling GetTimestamp(),
        * enabling the usage of DateTime.UtcNow instead.
        */
        static readonly bool _HighRes = Stopwatch.IsHighResolution;
        static readonly double _HighFrequency = 1000.0 / Stopwatch.Frequency;
        static readonly double _LowFrequency = 1000.0 / TimeSpan.TicksPerSecond;
        static bool _UseHRT;

        public static bool UsingHighResolutionTiming => _UseHRT && _HighRes && !Unix;
        public static long TickCount => (long)Ticks;
        public static double Ticks => _UseHRT && _HighRes && !Unix ? Stopwatch.GetTimestamp() * _HighFrequency : DateTime.UtcNow.Ticks * _LowFrequency;

        public static readonly bool Is64Bit = Environment.Is64BitProcess;
        public static bool MultiProcessor { get; private set; }
        public static int ProcessorCount { get; private set; }
        public static bool Unix { get; private set; }
    }
}