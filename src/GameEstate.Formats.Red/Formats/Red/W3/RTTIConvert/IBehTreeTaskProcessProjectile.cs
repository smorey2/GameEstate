using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class IBehTreeTaskProcessProjectile : IBehTreeTask
	{
		[Ordinal(1)] [RED("destroyProjectileOnDeactivate")] 		public CBool DestroyProjectileOnDeactivate { get; set;}

		[Ordinal(2)] [RED("combatDataStorage")] 		public CHandle<CHumanAICombatStorage> CombatDataStorage { get; set;}

		[Ordinal(3)] [RED("takeProjectile")] 		public CBool TakeProjectile { get; set;}

		[Ordinal(4)] [RED("projTemplate")] 		public CHandle<CEntityTemplate> ProjTemplate { get; set;}

		public IBehTreeTaskProcessProjectile(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new IBehTreeTaskProcessProjectile(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}