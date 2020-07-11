namespace GameEstate.Core
{
    public static class EstateBootstrap
    {
        static unsafe EstateBootstrap()
        {
            foreach (var startup in EstatePlatform.Startups)
                if (startup())
                    return;
            EstatePlatform.Platform = EstatePlatform.PlatformUnknown;
            CoreDebug.AssertFunc = x => System.Diagnostics.Debug.Assert(x);
            CoreDebug.LogFunc = a => System.Diagnostics.Debug.Print(a);
            CoreDebug.LogFormatFunc = (a, b) => System.Diagnostics.Debug.Print(a, b);
        }

        public static void Touch() { }
    }
}