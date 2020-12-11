using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SFurColor : CVariable
	{
		[Ordinal(1)] [RED("rootAlphaFalloff")] 		public CFloat RootAlphaFalloff { get; set;}

		[Ordinal(2)] [RED("rootColor")] 		public CColor RootColor { get; set;}

		[Ordinal(3)] [RED("rootColorTex")] 		public CHandle<CBitmapTexture> RootColorTex { get; set;}

		[Ordinal(4)] [RED("tipColor")] 		public CColor TipColor { get; set;}

		[Ordinal(5)] [RED("tipColorTex")] 		public CHandle<CBitmapTexture> TipColorTex { get; set;}

		[Ordinal(6)] [RED("rootTipColorWeight")] 		public CFloat RootTipColorWeight { get; set;}

		[Ordinal(7)] [RED("rootTipColorFalloff")] 		public CFloat RootTipColorFalloff { get; set;}

		[Ordinal(8)] [RED("strandTex")] 		public CHandle<CBitmapTexture> StrandTex { get; set;}

		[Ordinal(9)] [RED("strandBlendMode")] 		public CEnum<EHairStrandBlendModeType> StrandBlendMode { get; set;}

		[Ordinal(10)] [RED("strandBlendScale")] 		public CFloat StrandBlendScale { get; set;}

		[Ordinal(11)] [RED("textureBrightness")] 		public CFloat TextureBrightness { get; set;}

		[Ordinal(12)] [RED("ambientEnvScale")] 		public CFloat AmbientEnvScale { get; set;}

		public SFurColor(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SFurColor(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}