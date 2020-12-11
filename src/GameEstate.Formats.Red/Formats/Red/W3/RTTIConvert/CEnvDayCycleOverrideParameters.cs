using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CEnvDayCycleOverrideParameters : CVariable
	{
		[Ordinal(1)] [RED("fakeDayCycleEnable")] 		public CBool FakeDayCycleEnable { get; set;}

		[Ordinal(2)] [RED("fakeDayCycleHour")] 		public CFloat FakeDayCycleHour { get; set;}

		[Ordinal(3)] [RED("enableCustomSunRotation")] 		public CBool EnableCustomSunRotation { get; set;}

		[Ordinal(4)] [RED("customSunRotation")] 		public EulerAngles CustomSunRotation { get; set;}

		public CEnvDayCycleOverrideParameters(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CEnvDayCycleOverrideParameters(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}