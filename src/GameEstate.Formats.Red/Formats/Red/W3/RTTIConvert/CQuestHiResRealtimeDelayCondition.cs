using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CQuestHiResRealtimeDelayCondition : IQuestCondition
	{
		[Ordinal(1)] [RED("hours")] 		public CUInt32 Hours { get; set;}

		[Ordinal(2)] [RED("minutes")] 		public CUInt32 Minutes { get; set;}

		[Ordinal(3)] [RED("seconds")] 		public CUInt32 Seconds { get; set;}

		[Ordinal(4)] [RED("miliseconds")] 		public CUInt32 Miliseconds { get; set;}

		public CQuestHiResRealtimeDelayCondition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CQuestHiResRealtimeDelayCondition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}