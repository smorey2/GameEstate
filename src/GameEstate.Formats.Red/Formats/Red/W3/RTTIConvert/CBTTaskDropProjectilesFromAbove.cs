using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskDropProjectilesFromAbove : IBehTreeTask
	{
		[Ordinal(1)] [RED("resourceName")] 		public CName ResourceName { get; set;}

		[Ordinal(2)] [RED("activeOnAnimEvent")] 		public CName ActiveOnAnimEvent { get; set;}

		[Ordinal(3)] [RED("chanceToGuaranteePlayerHit")] 		public CFloat ChanceToGuaranteePlayerHit { get; set;}

		[Ordinal(4)] [RED("timeBetweenSpawn")] 		public CFloat TimeBetweenSpawn { get; set;}

		[Ordinal(5)] [RED("timeBetweenSpawnRandomizationPerc")] 		public CFloat TimeBetweenSpawnRandomizationPerc { get; set;}

		[Ordinal(6)] [RED("minDistFromTarget")] 		public CFloat MinDistFromTarget { get; set;}

		[Ordinal(7)] [RED("maxDistFromTarget")] 		public CFloat MaxDistFromTarget { get; set;}

		[Ordinal(8)] [RED("minDistFromEachOther")] 		public CFloat MinDistFromEachOther { get; set;}

		[Ordinal(9)] [RED("minYOffset")] 		public CFloat MinYOffset { get; set;}

		[Ordinal(10)] [RED("maxYOffset")] 		public CFloat MaxYOffset { get; set;}

		[Ordinal(11)] [RED("useCombatTarget")] 		public CBool UseCombatTarget { get; set;}

		[Ordinal(12)] [RED("useOwnerAsTarget")] 		public CBool UseOwnerAsTarget { get; set;}

		[Ordinal(13)] [RED("target")] 		public CHandle<CActor> Target { get; set;}

		[Ordinal(14)] [RED("entityTemplate")] 		public CHandle<CEntityTemplate> EntityTemplate { get; set;}

		[Ordinal(15)] [RED("usedPos", 2,0)] 		public CArray<Vector> UsedPos { get; set;}

		[Ordinal(16)] [RED("activated")] 		public CBool Activated { get; set;}

		public CBTTaskDropProjectilesFromAbove(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskDropProjectilesFromAbove(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}