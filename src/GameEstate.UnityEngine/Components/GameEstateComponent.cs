using GameEstate;
using static GameEstate.EstateDebug;

public class GameEstateComponent : UnityEngine.MonoBehaviour
{
    public static void Awake()
    {
        EstatePlatform.Startups.Add(UnityPlatform.Startup);
        Log("Started");
    }
}