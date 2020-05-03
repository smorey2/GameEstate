﻿using GameEstate.Core;
using System.Collections.Generic;
using System.IO;

namespace GameEstate.Formats.Binary.Tes.Records
{
    public class LEVCRecord : Record
    {
        public override string ToString() => $"LEVC: {EDID.Value}";
        public STRVField EDID { get; set; } // Editor ID
        public IN32Field DATA; // List data - 1 = Calc from all levels <= PC level
        public BYTEField NNAM; // Chance None?
        public IN32Field INDX; // Number of items in list
        public List<STRVField> CNAMs = new List<STRVField>(); // ID string of list item
        public List<IN16Field> INTVs = new List<IN16Field>(); // PC level for previous CNAM
        // The CNAM/INTV can occur many times in pairs

        public override bool CreateField(BinaryReader r, TesFormat format, string type, int dataSize)
        {
            if (format == TesFormat.TES3)
                switch (type)
                {
                    case "NAME": EDID = r.ReadSTRV(dataSize); return true;
                    case "DATA": DATA = r.ReadT<IN32Field>(dataSize); return true;
                    case "NNAM": NNAM = r.ReadT<BYTEField>(dataSize); return true;
                    case "INDX": INDX = r.ReadT<IN32Field>(dataSize); return true;
                    case "CNAM": CNAMs.Add(r.ReadSTRV(dataSize)); return true;
                    case "INTV": INTVs.Add(r.ReadT<IN16Field>(dataSize)); return true;
                    default: return false;
                }
            return false;
        }
    }
}