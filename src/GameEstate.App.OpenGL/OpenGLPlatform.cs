namespace GameEstate
{
    public static class OpenGLPlatform
    {
        public static unsafe bool Startup()
        {
            try
            {
                EstatePlatform.Platform = "OpenGL";
                return true;
            }
            catch { return false; }
        }
    }
}