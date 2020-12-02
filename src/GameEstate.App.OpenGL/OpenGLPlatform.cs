﻿namespace GameEstate
{
    public static class OpenGLPlatform
    {
        public static unsafe bool Startup()
        {
            try
            {
                EstatePlatform.Platform = "OpenGL";
                EstatePlatform.GraphicFactory = source => new OpenGLGraphic(source);
                EstateDebug.AssertFunc = x => System.Diagnostics.Debug.Assert(x);
                EstateDebug.LogFunc = a => System.Diagnostics.Debug.Print(a);
                EstateDebug.LogFormatFunc = (a, b) => System.Diagnostics.Debug.Print(a, b);
                return true;
            }
            catch { return false; }
        }
    }
}