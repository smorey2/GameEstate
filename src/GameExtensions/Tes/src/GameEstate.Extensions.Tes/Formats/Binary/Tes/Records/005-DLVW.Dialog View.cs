﻿using GameEstate.Core;
using System.IO;

namespace GameEstate.Formats.Binary.Tes.Records
{
    public class DLVWRecord : Record
    {
        public override string ToString() => $"DLVW: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public CREFField CNAME; // RGB color

        public override bool CreateField(BinaryReader r, TesFormat format, string type, int dataSize)
        {
            switch (type)
            {
                case "EDID": EDID = r.ReadSTRV(dataSize); return true;
                case "CNAME": CNAME = r.ReadT<CREFField>(dataSize); return true;
                default: return false;
            }
        }
    }
}