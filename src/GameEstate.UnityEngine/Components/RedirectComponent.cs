// CRY
//using Redirect = GameEstate.Estates.Cry.Components.LoadAsset;

// TES
using Redirect = GameEstate.Estates.Tes.Components.TestAsset;
//using Redirect = GameEstate.Estates.Tes.Components.LoadData;
//using Redirect = GameEstate.Estates.Tes.Components.LoadEngine;

// ULTIMA
//using Redirect = GameEstate.Estates.UO.Components.LoadAsset;
//using Redirect = GameEstate.Estates.UO.Components.LoadData;
//using Redirect = GameEstate.Estates.UO.Components.LoadEngine;

// ULTIMAIX
//using Redirect = GameEstate.Estates.U9.Components.LoadAsset;
//using Redirect = GameEstate.Estates.U9.Components.LoadData;
//using Redirect = GameEstate.Estates.U9.Components.LoadEngine;

public class RedirectComponent : UnityEngine.MonoBehaviour
{
    public static void Awake() => Redirect.Awake();
    public static void Start() => Redirect.Start();
    public static void OnDestroy() => Redirect.OnDestroy();
    public static void Update() => Redirect.Update();
}