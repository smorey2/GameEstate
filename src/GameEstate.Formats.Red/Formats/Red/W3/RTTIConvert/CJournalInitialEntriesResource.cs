using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CJournalInitialEntriesResource : CResource
	{
		[Ordinal(1)] [RED("entries", 2,0)] 		public CArray<CHandle<CJournalPath>> Entries { get; set;}

		[Ordinal(2)] [RED("regularQuestCount")] 		public CUInt32 RegularQuestCount { get; set;}

		[Ordinal(3)] [RED("monsterHuntQuestCount")] 		public CUInt32 MonsterHuntQuestCount { get; set;}

		[Ordinal(4)] [RED("treasureHuntQuestCount")] 		public CUInt32 TreasureHuntQuestCount { get; set;}

		public CJournalInitialEntriesResource(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CJournalInitialEntriesResource(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}