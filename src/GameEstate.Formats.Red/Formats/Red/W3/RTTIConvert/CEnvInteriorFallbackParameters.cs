using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CEnvInteriorFallbackParameters : CVariable
	{
		[Ordinal(1)] [RED("activated")] 		public CBool Activated { get; set;}

		[Ordinal(2)] [RED("colorAmbientMul")] 		public SSimpleCurve ColorAmbientMul { get; set;}

		[Ordinal(3)] [RED("colorReflectionLow")] 		public SSimpleCurve ColorReflectionLow { get; set;}

		[Ordinal(4)] [RED("colorReflectionMiddle")] 		public SSimpleCurve ColorReflectionMiddle { get; set;}

		[Ordinal(5)] [RED("colorReflectionHigh")] 		public SSimpleCurve ColorReflectionHigh { get; set;}

		public CEnvInteriorFallbackParameters(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CEnvInteriorFallbackParameters(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}