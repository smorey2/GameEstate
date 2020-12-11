using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskToadYrdenDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("npc")] 		public CHandle<CActor> Npc { get; set;}

		[Ordinal(2)] [RED("leftYrden")] 		public CBool LeftYrden { get; set;}

		[Ordinal(3)] [RED("leaveAfter")] 		public CFloat LeaveAfter { get; set;}

		[Ordinal(4)] [RED("enterTimestamp")] 		public CFloat EnterTimestamp { get; set;}

		[Ordinal(5)] [RED("l_effect")] 		public CBool L_effect { get; set;}

		public CBTTaskToadYrdenDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskToadYrdenDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}