using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CQuestSpawnNotStreamedBoatBlock : CQuestGraphBlock
	{
		[Ordinal(1)] [RED("spawnPointTag")] 		public CName SpawnPointTag { get; set;}

		[Ordinal(2)] [RED("tagsToSet", 2,0)] 		public CArray<CName> TagsToSet { get; set;}

		[Ordinal(3)] [RED("spawnLayerTag")] 		public CName SpawnLayerTag { get; set;}

		[Ordinal(4)] [RED("forceNonStreamed")] 		public CBool ForceNonStreamed { get; set;}

		public CQuestSpawnNotStreamedBoatBlock(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CQuestSpawnNotStreamedBoatBlock(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}