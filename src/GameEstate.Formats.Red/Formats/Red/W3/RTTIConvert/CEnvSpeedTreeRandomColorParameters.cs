using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CEnvSpeedTreeRandomColorParameters : CVariable
	{
		[Ordinal(1)] [RED("luminanceWeights")] 		public SSimpleCurve LuminanceWeights { get; set;}

		[Ordinal(2)] [RED("randomColor0")] 		public SSimpleCurve RandomColor0 { get; set;}

		[Ordinal(3)] [RED("saturation0")] 		public SSimpleCurve Saturation0 { get; set;}

		[Ordinal(4)] [RED("randomColor1")] 		public SSimpleCurve RandomColor1 { get; set;}

		[Ordinal(5)] [RED("saturation1")] 		public SSimpleCurve Saturation1 { get; set;}

		[Ordinal(6)] [RED("randomColor2")] 		public SSimpleCurve RandomColor2 { get; set;}

		[Ordinal(7)] [RED("saturation2")] 		public SSimpleCurve Saturation2 { get; set;}

		public CEnvSpeedTreeRandomColorParameters(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CEnvSpeedTreeRandomColorParameters(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}