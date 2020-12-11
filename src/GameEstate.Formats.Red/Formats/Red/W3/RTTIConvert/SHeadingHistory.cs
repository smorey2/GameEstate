using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SHeadingHistory : CVariable
	{
		[Ordinal(1)] [RED("time")] 		public EngineTime Time { get; set;}

		[Ordinal(2)] [RED("headValue")] 		public CFloat HeadValue { get; set;}

		[Ordinal(3)] [RED("speedValue")] 		public CFloat SpeedValue { get; set;}

		public SHeadingHistory(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SHeadingHistory(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}