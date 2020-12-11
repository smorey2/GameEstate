using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CEnvCameraLightParameters : CVariable
	{
		[Ordinal(1)] [RED("activated")] 		public CBool Activated { get; set;}

		[Ordinal(2)] [RED("color")] 		public SSimpleCurve Color { get; set;}

		[Ordinal(3)] [RED("attenuation")] 		public SSimpleCurve Attenuation { get; set;}

		[Ordinal(4)] [RED("radius")] 		public SSimpleCurve Radius { get; set;}

		[Ordinal(5)] [RED("offsetFront")] 		public SSimpleCurve OffsetFront { get; set;}

		[Ordinal(6)] [RED("offsetRight")] 		public SSimpleCurve OffsetRight { get; set;}

		[Ordinal(7)] [RED("offsetUp")] 		public SSimpleCurve OffsetUp { get; set;}

		public CEnvCameraLightParameters(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CEnvCameraLightParameters(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}