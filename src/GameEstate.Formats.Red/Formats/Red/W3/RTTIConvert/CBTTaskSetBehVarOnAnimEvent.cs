using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskSetBehVarOnAnimEvent : IBehTreeTask
	{
		[Ordinal(1)] [RED("npc")] 		public CHandle<CNewNPC> Npc { get; set;}

		[Ordinal(2)] [RED("eventName")] 		public CName EventName { get; set;}

		[Ordinal(3)] [RED("behVarName")] 		public CName BehVarName { get; set;}

		[Ordinal(4)] [RED("behVarValue")] 		public CFloat BehVarValue { get; set;}

		[Ordinal(5)] [RED("eventReceived")] 		public CBool EventReceived { get; set;}

		[Ordinal(6)] [RED("onDurationEvent")] 		public CBool OnDurationEvent { get; set;}

		[Ordinal(7)] [RED("behValueOnDurationEventStart")] 		public CFloat BehValueOnDurationEventStart { get; set;}

		[Ordinal(8)] [RED("behValueOnDurationEventEnd")] 		public CFloat BehValueOnDurationEventEnd { get; set;}

		public CBTTaskSetBehVarOnAnimEvent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskSetBehVarOnAnimEvent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}