using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeNodeCombatTicketRequestDefinition : IBehTreeNodeCombatTicketDecoratorBaseDefinition
	{
		[Ordinal(1)] [RED("ticketRequestValidTime")] 		public CFloat TicketRequestValidTime { get; set;}

		[Ordinal(2)] [RED("requestOnCompletion")] 		public CBool RequestOnCompletion { get; set;}

		[Ordinal(3)] [RED("requestOnInterruption")] 		public CBool RequestOnInterruption { get; set;}

		[Ordinal(4)] [RED("requestWhileActive")] 		public CBool RequestWhileActive { get; set;}

		public CBehTreeNodeCombatTicketRequestDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeNodeCombatTicketRequestDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}