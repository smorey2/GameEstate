namespace GameEstate.Core
{
    public class CoreEstate<TFileManager, TGame>
        where TFileManager : CoreFileManager<TFileManager, TGame>, new()
        where TGame : struct
    {
        public CoreFileManager<TFileManager, TGame> FileManager;
    }
}