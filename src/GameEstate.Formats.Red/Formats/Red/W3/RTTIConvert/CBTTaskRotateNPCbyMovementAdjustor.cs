using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskRotateNPCbyMovementAdjustor : IBehTreeTask
	{
		[Ordinal(1)] [RED("npc")] 		public CHandle<CNewNPC> Npc { get; set;}

		[Ordinal(2)] [RED("target")] 		public CHandle<CActor> Target { get; set;}

		[Ordinal(3)] [RED("active")] 		public CBool Active { get; set;}

		[Ordinal(4)] [RED("onAnimEvent")] 		public CBool OnAnimEvent { get; set;}

		[Ordinal(5)] [RED("eventName")] 		public CName EventName { get; set;}

		[Ordinal(6)] [RED("finishTaskOnAllowBlend")] 		public CBool FinishTaskOnAllowBlend { get; set;}

		public CBTTaskRotateNPCbyMovementAdjustor(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskRotateNPCbyMovementAdjustor(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}