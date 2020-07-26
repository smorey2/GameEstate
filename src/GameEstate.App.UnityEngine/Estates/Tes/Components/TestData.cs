using GameEstate.Core;
using System;

namespace GameEstate.Estates.Tes.Components
{
    public static class LoadData
    {
        static Estate Estate = EstateManager.GetEstate("Tes");
        static AbstractPakFile PakFile;

        public static void Awake() { }
        public static void Start()
        {
            var pakUri = new Uri("game:/Morrowind.esm#Morrowind");
            //var pakUri = new Uri("game:/Bloodmoon.esm#Morrowind");
            //var pakUri = new Uri("game:/Tribunal.esm#Morrowind");
            //var pakUri = new Uri("game:/Oblivion.esm#Oblivion");
            //var pakUri = new Uri("game:/Skyrim.esm#SkyrimVR");
            //var pakUri = new Uri("game:/Fallout4.esm#Fallout4");
            //var pakUri = new Uri("game:/Fallout4.esm#Fallout4VR");
            PakFile = Estate.OpenPakFile(pakUri);

            //TestLoadCell(new Vector3(((-2 << 5) + 1) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, ((-1 << 5) + 1) * ConvertUtils.ExteriorCellSideLengthInMeters));
            //TestLoadCell(new Vector3((-1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (-1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters));
            //TestLoadCell(new Vector3(0 * ConvertUtils.ExteriorCellSideLengthInMeters, 0, 0 * ConvertUtils.ExteriorCellSideLengthInMeters));
            //TestLoadCell(new Vector3((1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (1 << 3) * ConvertUtils.ExteriorCellSideLengthInMeters));
            //TestLoadCell(new Vector3((1 << 5) * ConvertUtils.ExteriorCellSideLengthInMeters, 0, (1 << 5) * ConvertUtils.ExteriorCellSideLengthInMeters));
            //TestAllCells();
        }
        public static void OnDestroy() { PakFile?.Dispose(); PakFile = null; }
        public static void Update() { }

        //public static Int3 GetCellId(Vector3 point, int world) => new Int3(Mathf.FloorToInt(point.x / ConvertUtils.ExteriorCellSideLengthInMeters), Mathf.FloorToInt(point.z / ConvertUtils.ExteriorCellSideLengthInMeters), world);

        //static void TestLoadCell(Vector3 position)
        //{
        //    var cellId = GetCellId(position, 60);
        //    var cell = DatFile.FindCellRecord(cellId);
        //    var land = ((TesDataPack)DatFile).FindLANDRecord(cellId);
        //    Log($"LAND #{land?.Id}");
        //}

        //static void TestAllCells()
        //{
        //    var cells = ((TesDataPack)DatFile).GroupByLabel["CELL"].Records;
        //    Log($"CELLS: {cells.Count}");
        //    foreach (var record in cells.Cast<CELLRecord>())
        //        Log(record.EDID.Value);
        //}
    }
}
