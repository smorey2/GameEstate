using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTTaskSummonCreaturesOnSpotsDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("entityToSpawn")] 		public CHandle<CEntityTemplate> EntityToSpawn { get; set;}

		[Ordinal(2)] [RED("summonOnAnimEvent")] 		public CName SummonOnAnimEvent { get; set;}

		[Ordinal(3)] [RED("spotTag")] 		public CName SpotTag { get; set;}

		[Ordinal(4)] [RED("minDistance")] 		public CFloat MinDistance { get; set;}

		[Ordinal(5)] [RED("maxDistance")] 		public CFloat MaxDistance { get; set;}

		[Ordinal(6)] [RED("maxSpawnQuantity")] 		public CInt32 MaxSpawnQuantity { get; set;}

		[Ordinal(7)] [RED("betweenSpawnDelay")] 		public SRangeF BetweenSpawnDelay { get; set;}

		[Ordinal(8)] [RED("completeAfterSpawn")] 		public CBool CompleteAfterSpawn { get; set;}

		[Ordinal(9)] [RED("spawnAreaCenter")] 		public CEnum<ETargetName> SpawnAreaCenter { get; set;}

		[Ordinal(10)] [RED("minDistanceFromSpawner")] 		public CFloat MinDistanceFromSpawner { get; set;}

		[Ordinal(11)] [RED("spawnBehVarName")] 		public CName SpawnBehVarName { get; set;}

		[Ordinal(12)] [RED("spawnBehVar")] 		public CFloat SpawnBehVar { get; set;}

		[Ordinal(13)] [RED("shouldForceBehaviorOnSpawn")] 		public CBool ShouldForceBehaviorOnSpawn { get; set;}

		public BTTaskSummonCreaturesOnSpotsDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTTaskSummonCreaturesOnSpotsDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}