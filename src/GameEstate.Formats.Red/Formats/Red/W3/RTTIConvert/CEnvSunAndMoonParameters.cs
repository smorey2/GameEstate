using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CEnvSunAndMoonParameters : CVariable
	{
		[Ordinal(1)] [RED("activated")] 		public CBool Activated { get; set;}

		[Ordinal(2)] [RED("sunSize")] 		public SSimpleCurve SunSize { get; set;}

		[Ordinal(3)] [RED("sunColor")] 		public SSimpleCurve SunColor { get; set;}

		[Ordinal(4)] [RED("sunFlareSize")] 		public SSimpleCurve SunFlareSize { get; set;}

		[Ordinal(5)] [RED("sunFlareColor")] 		public CEnvFlareColorParameters SunFlareColor { get; set;}

		[Ordinal(6)] [RED("moonSize")] 		public SSimpleCurve MoonSize { get; set;}

		[Ordinal(7)] [RED("moonColor")] 		public SSimpleCurve MoonColor { get; set;}

		[Ordinal(8)] [RED("moonFlareSize")] 		public SSimpleCurve MoonFlareSize { get; set;}

		[Ordinal(9)] [RED("moonFlareColor")] 		public CEnvFlareColorParameters MoonFlareColor { get; set;}

		public CEnvSunAndMoonParameters(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CEnvSunAndMoonParameters(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}