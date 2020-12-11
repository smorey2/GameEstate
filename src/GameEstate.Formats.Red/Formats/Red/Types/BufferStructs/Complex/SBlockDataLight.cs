﻿using FastMember;
using GameEstate.Formats.Red.CR2W;

namespace GameEstate.Formats.Red.Types.BufferStructs.Complex
{
    [REDMeta(EREDMetaInfo.REDStruct)]
    class SBlockDataLight : CVariable
    {
        [Ordinal(0), RED] public CUInt32 color { get; set; }
        [Ordinal(1), RED] public CFloat radius { get; set; }
        [Ordinal(2), RED] public CFloat brightness { get; set; }
        [Ordinal(3), RED] public CFloat attenuation { get; set; }
        [Ordinal(4), RED] public CFloat autoHideRange { get; set; }
        [Ordinal(5), RED] public CFloat shadowFadeDistance { get; set; }
        [Ordinal(6), RED] public CFloat shadowFadeRange { get; set; }
        [Ordinal(7), RED] public CFloat shadowFadeBlendFactor { get; set; }
        [Ordinal(8), RED] public SVector3D lightFlickering { get; set; }
        [Ordinal(9), RED] public CUInt8 shadowCastingMode { get; set; }
        [Ordinal(10), RED] public CUInt8 dynamicShadowsFaceMask { get; set; }
        [Ordinal(11), RED] public CUInt8 envColorGroup { get; set; }
        [Ordinal(12), RED] public CUInt8 padding { get; set; }
        [Ordinal(13), RED] public CUInt32 lightUsageMask { get; set; }

        public SBlockDataLight(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name) { }
    }
}
