using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAINpcUnconsciousParams : CAIDeathParameters
	{
		[Ordinal(1)] [RED("unconsciousDuration")] 		public CFloat UnconsciousDuration { get; set;}

		[Ordinal(2)] [RED("unconsciousGetUpDist")] 		public CFloat UnconsciousGetUpDist { get; set;}

		public CAINpcUnconsciousParams(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAINpcUnconsciousParams(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}