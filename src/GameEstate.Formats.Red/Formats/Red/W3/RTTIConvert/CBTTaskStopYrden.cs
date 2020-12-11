using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskStopYrden : IBehTreeTask
	{
		[Ordinal(1)] [RED("npc")] 		public CHandle<CNewNPC> Npc { get; set;}

		[Ordinal(2)] [RED("yrden")] 		public CHandle<W3YrdenEntity> Yrden { get; set;}

		[Ordinal(3)] [RED("yrdenIsActionTarget")] 		public CBool YrdenIsActionTarget { get; set;}

		[Ordinal(4)] [RED("range")] 		public CFloat Range { get; set;}

		[Ordinal(5)] [RED("useYrdenRadiusAsRange")] 		public CBool UseYrdenRadiusAsRange { get; set;}

		[Ordinal(6)] [RED("maxResults")] 		public CInt32 MaxResults { get; set;}

		[Ordinal(7)] [RED("onActivate")] 		public CBool OnActivate { get; set;}

		[Ordinal(8)] [RED("onDeactivate")] 		public CBool OnDeactivate { get; set;}

		[Ordinal(9)] [RED("onAnimEvent")] 		public CBool OnAnimEvent { get; set;}

		[Ordinal(10)] [RED("eventName")] 		public CName EventName { get; set;}

		[Ordinal(11)] [RED("stopYrdenShock")] 		public CBool StopYrdenShock { get; set;}

		public CBTTaskStopYrden(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskStopYrden(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}