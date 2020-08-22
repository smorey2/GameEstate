//using GameEstate.Core;
//using System;

//namespace GameEstate.Estates.Tes.Components
//{
//    public static class LoadCell
//    {
//        static Estate Estate = EstateManager.GetEstate("Tes");
//        static UnityPakFile PakFile = new UnityPakFile(Estate.OpenPakFile(new Uri("game:/Morrowind.bsa#Morrowind")));
//        //static TesUnityPakFile PakFile = new TesUnityPakFile(Estate.OpenPakFile(new Uri("game:/Bloodmoon.bsa#Morrowind")));
//        //static TesUnityPakFile PakFile = new TesUnityPakFile(Estate.OpenPakFile(new Uri("game:/Tribunal.bsa#Morrowind")));
//        //static TesUnityPakFile PakFile = new TesUnityPakFile(Estate.OpenPakFile(new Uri("game:/Oblivion.bsa#Oblivion")));
//        //static TesUnityPakFile PakFile = new TesUnityPakFile(Estate.OpenPakFile(new Uri("game:/Skyrim.esm#SkyrimVR")));
//        //static TesUnityPakFile PakFile = new TesUnityPakFile(Estate.OpenPakFile(new Uri("game:/Fallout4.esm#Fallout4")));
//        //static TesUnityPakFile PakFile = new TesUnityPakFile(Estate.OpenPakFile(new Uri("Fallout4.esm#Fallout4VR")));

//        public static void Awake() { }
//        public static void Start()
//        {
//            //TestLoadCell(new Vector3(((-2 << 5) + 1) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, ((-1 << 5) + 1) * ConvertUtils.ExteriorCellSideLengthInMeters));
//            //TestLoadCell(new Vector3((-1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (-1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters));
//            //TestLoadCell(new Vector3(0 * ConvertUtils.ExteriorCellSideLengthInMeters, 0, 0 * ConvertUtils.ExteriorCellSideLengthInMeters));
//            //TestLoadCell(new Vector3((1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters));
//            //TestLoadCell(new Vector3((1 << 5) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (1 << 5) * ConvertUtils.ExteriorCellSideLengthInMeters));
//            //TestAllCells();
//        }
//        public static void OnDestroy() { PakFile?.Dispose(); PakFile = null; }
//        public static void Update() { }

//        //public static Int3 GetCellId(Vector3 point, int world) => new Int3(Mathf.FloorToInt(point.x / ConvertUtils.ExteriorCellSideLengthInMeters), Mathf.FloorToInt(point.z / ConvertUtils.ExteriorCellSideLengthInMeters), world);

//        //static void TestLoadCell(Vector3 position)
//        //{
//        //    var cellId = GetCellId(position, 60);
//        //    var cell = DatFile.FindCellRecord(cellId);
//        //    var land = ((TesDataPack)DatFile).FindLANDRecord(cellId);
//        //    Log($"LAND #{land?.Id}");
//        //}

//        //static void TestAllCells()
//        //{
//        //    var cells = ((TesDataPack)DatFile).GroupByLabel["CELL"].Records;
//        //    Log($"CELLS: {cells.Count}");
//        //    foreach (var record in cells.Cast<CELLRecord>())
//        //        Log(record.EDID.Value);
//        //}
//    }
//}
