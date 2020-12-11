using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3QuestCond_PlayerSkillPoints : CQuestScriptedCondition
	{
		[Ordinal(1)] [RED("freeSkillPoints")] 		public CInt32 FreeSkillPoints { get; set;}

		[Ordinal(2)] [RED("comparator")] 		public CEnum<ECompareOp> Comparator { get; set;}

		public W3QuestCond_PlayerSkillPoints(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3QuestCond_PlayerSkillPoints(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}