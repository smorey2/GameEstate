﻿using FastMember;
using GameEstate.Formats.Red.CR2W;
using GameEstate.Formats.Red.Types.Arrays;
using GameEstate.Formats.Red.Types.BufferStructs.Complex;
using System.IO;
using System.Runtime.Serialization;
using static GameEstate.Formats.Red.Types.Enums;

namespace GameEstate.Formats.Red.Types
{
    [DataContract(Namespace = ""), REDMeta]
    public class SCurveData : CVariable
    {
        [Ordinal(1), RED("Curve Values", 142, 0)] public CArray<SCurveDataEntry> Curve_Values { get; set; }
        [Ordinal(2), RED("value type")] public CEnum<ECurveValueType> Value_type { get; set; }
        [Ordinal(3), RED("type")] public CEnum<ECurveBaseType> Type { get; set; }
        [Ordinal(4), RED("is looped")] public CBool Is_looped { get; set; }

        public SCurveData(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name) { }

        public override void Read(BinaryReader r, uint size) => base.Read(r, size);

        public override void Write(BinaryWriter w) => base.Write(w);

        public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SCurveData(cr2w, parent, name);
    }

    [DataContract(Namespace = ""), REDMeta(EREDMetaInfo.REDStruct)]
    public class SCurveBufferData : CVariable
    {
        [Ordinal(1), REDBuffer] public CFloat time { get; set; }
        [Ordinal(2), REDBuffer] public SVector4D controlPoint1 { get; set; }
        [Ordinal(3), REDBuffer] public SVector4D controlPoint2 { get; set; }
        [Ordinal(4), REDBuffer] public CFloat value { get; set; }
        [Ordinal(5), REDBuffer] public CUInt16 curveTypeL { get; set; }
        [Ordinal(6), REDBuffer] public CUInt16 curveTypeR { get; set; }
        [Ordinal(7), REDBuffer] public CUInt32 unk1 { get; set; }

        public SCurveBufferData(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name) { }

        public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SCurveBufferData(cr2w, parent, name);
    }
}
