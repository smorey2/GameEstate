using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBaseQuestScriptedActionsBlock : CQuestGraphBlock
	{
		[Ordinal(1)] [RED("npcTag")] 		public CName NpcTag { get; set;}

		[Ordinal(2)] [RED("handleBehaviorOutcome")] 		public CBool HandleBehaviorOutcome { get; set;}

		[Ordinal(3)] [RED("actionsPriority")] 		public CEnum<ETopLevelAIPriorities> ActionsPriority { get; set;}

		[Ordinal(4)] [RED("onlyOneActor")] 		public CBool OnlyOneActor { get; set;}

		public CBaseQuestScriptedActionsBlock(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBaseQuestScriptedActionsBlock(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}