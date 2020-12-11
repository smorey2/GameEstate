using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3SpawnMeteor : W3AdvancedProjectile
	{
		[Ordinal(1)] [RED("initFxName")] 		public CName InitFxName { get; set;}

		[Ordinal(2)] [RED("onCollisionFxName")] 		public CName OnCollisionFxName { get; set;}

		[Ordinal(3)] [RED("onCollisionFxName2")] 		public CName OnCollisionFxName2 { get; set;}

		[Ordinal(4)] [RED("startFxName")] 		public CName StartFxName { get; set;}

		[Ordinal(5)] [RED("ent")] 		public CHandle<CEntity> Ent { get; set;}

		[Ordinal(6)] [RED("projectileHitGround")] 		public CBool ProjectileHitGround { get; set;}

		[Ordinal(7)] [RED("playerPos")] 		public Vector PlayerPos { get; set;}

		[Ordinal(8)] [RED("projPos")] 		public Vector ProjPos { get; set;}

		[Ordinal(9)] [RED("projSpawnPos")] 		public Vector ProjSpawnPos { get; set;}

		public W3SpawnMeteor(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3SpawnMeteor(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}