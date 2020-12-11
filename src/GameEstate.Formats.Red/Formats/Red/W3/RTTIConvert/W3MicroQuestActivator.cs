using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3MicroQuestActivator : CGameplayEntity
	{
		[Ordinal(1)] [RED("microQuestEntries", 2,0)] 		public CArray<EncounterEntryDetails> MicroQuestEntries { get; set;}

		[Ordinal(2)] [RED("selectedEntriesList", 2,0)] 		public CArray<EncounterEntryDetails> SelectedEntriesList { get; set;}

		[Ordinal(3)] [RED("chosenMicroQuestTag")] 		public CName ChosenMicroQuestTag { get; set;}

		[Ordinal(4)] [RED("isPlayerInArea")] 		public CBool IsPlayerInArea { get; set;}

		public W3MicroQuestActivator(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3MicroQuestActivator(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}