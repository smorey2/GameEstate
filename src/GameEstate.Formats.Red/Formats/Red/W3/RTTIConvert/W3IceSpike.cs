using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3IceSpike : W3DurationObstacle
	{
		[Ordinal(1)] [RED("damageValue")] 		public CFloat DamageValue { get; set;}

		[Ordinal(2)] [RED("weaponSlot")] 		public CName WeaponSlot { get; set;}

		[Ordinal(3)] [RED("canBeDestroyed")] 		public CBool CanBeDestroyed { get; set;}

		[Ordinal(4)] [RED("destroyAfterTime")] 		public CFloat DestroyAfterTime { get; set;}

		[Ordinal(5)] [RED("delayToDealDamage")] 		public CFloat DelayToDealDamage { get; set;}

		public W3IceSpike(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3IceSpike(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}