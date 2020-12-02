namespace GameEstate
{
    public static class TestPlatform
    {
        public static unsafe bool Startup()
        {
            try
            {
                EstatePlatform.Platform = "Test";
                EstatePlatform.GraphicFactory = source => new TestGraphic(source);
                EstateDebug.AssertFunc = x => System.Diagnostics.Debug.Assert(x);
                EstateDebug.LogFunc = a => System.Diagnostics.Debug.Print(a);
                EstateDebug.LogFormatFunc = (a, b) => System.Diagnostics.Debug.Print(a, b);
                return true;
            }
            catch { return false; }
        }
    }
}