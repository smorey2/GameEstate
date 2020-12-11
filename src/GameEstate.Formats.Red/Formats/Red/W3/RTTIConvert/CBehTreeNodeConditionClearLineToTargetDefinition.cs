using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeNodeConditionClearLineToTargetDefinition : CBehTreeNodeConditionDefinition
	{
		[Ordinal(1)] [RED("combatTarget")] 		public CBool CombatTarget { get; set;}

		[Ordinal(2)] [RED("navTest")] 		public CBool NavTest { get; set;}

		[Ordinal(3)] [RED("creatureTest")] 		public CBool CreatureTest { get; set;}

		[Ordinal(4)] [RED("useAgentRadius")] 		public CBool UseAgentRadius { get; set;}

		[Ordinal(5)] [RED("customRadius")] 		public CBehTreeValFloat CustomRadius { get; set;}

		public CBehTreeNodeConditionClearLineToTargetDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeNodeConditionClearLineToTargetDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}