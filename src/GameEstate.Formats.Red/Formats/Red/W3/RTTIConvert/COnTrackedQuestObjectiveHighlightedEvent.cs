using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class COnTrackedQuestObjectiveHighlightedEvent : CHudEvent
	{
		[Ordinal(1)] [RED("journalObjective")] 		public CHandle<CJournalQuestObjective> JournalObjective { get; set;}

		[Ordinal(2)] [RED("journalObjectiveIndex")] 		public CInt32 JournalObjectiveIndex { get; set;}

		public COnTrackedQuestObjectiveHighlightedEvent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new COnTrackedQuestObjectiveHighlightedEvent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}