using System;

namespace GameEstate.Core
{
    public class CoreDebug
    {
        static CoreDebug() { _ = UnsafeUtils.Platform; }

        public static Action<bool> AssertFunc;
        public static Action<string> LogFunc;
        public static Action<string, object[]> LogFormatFunc;
        public static void Assert(bool condition) => AssertFunc(condition);
        public static void Log() { }
        public static void Log(string format) => LogFunc(format);
        public static void LogFormat(string format, params object[] args) => LogFormatFunc(format, args);
    }
}