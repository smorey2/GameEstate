using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CQuestPokeScriptedActionsBlock : CQuestGraphBlock
	{
		[Ordinal(1)] [RED("npcTag")] 		public CName NpcTag { get; set;}

		[Ordinal(2)] [RED("pokeEvent")] 		public CName PokeEvent { get; set;}

		[Ordinal(3)] [RED("handleBehaviorOutcome")] 		public CBool HandleBehaviorOutcome { get; set;}

		[Ordinal(4)] [RED("onlyOneActor")] 		public CBool OnlyOneActor { get; set;}

		public CQuestPokeScriptedActionsBlock(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CQuestPokeScriptedActionsBlock(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}