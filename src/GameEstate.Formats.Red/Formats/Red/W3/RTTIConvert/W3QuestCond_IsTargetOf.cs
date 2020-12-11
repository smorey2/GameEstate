using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3QuestCond_IsTargetOf : CQCActorScriptedCondition
	{
		[Ordinal(1)] [RED("attackerTag")] 		public CName AttackerTag { get; set;}

		[Ordinal(2)] [RED("attacker")] 		public CHandle<CActor> Attacker { get; set;}

		[Ordinal(3)] [RED("listener")] 		public CHandle<W3QuestCond_IsTargetOf_Listener> Listener { get; set;}

		public W3QuestCond_IsTargetOf(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3QuestCond_IsTargetOf(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}