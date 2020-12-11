using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3Action_Attack : W3DamageAction
	{
		[Ordinal(1)] [RED("weaponId")] 		public SItemUniqueId WeaponId { get; set;}

		[Ordinal(2)] [RED("crossbowId")] 		public SItemUniqueId CrossbowId { get; set;}

		[Ordinal(3)] [RED("attackName")] 		public CName AttackName { get; set;}

		[Ordinal(4)] [RED("attackTypeName")] 		public CName AttackTypeName { get; set;}

		[Ordinal(5)] [RED("isAttackReflected")] 		public CBool IsAttackReflected { get; set;}

		[Ordinal(6)] [RED("isParried")] 		public CBool IsParried { get; set;}

		[Ordinal(7)] [RED("isCountered")] 		public CBool IsCountered { get; set;}

		[Ordinal(8)] [RED("attackAnimName")] 		public CName AttackAnimName { get; set;}

		[Ordinal(9)] [RED("hitTime")] 		public CFloat HitTime { get; set;}

		[Ordinal(10)] [RED("weaponEntity")] 		public CHandle<CItemEntity> WeaponEntity { get; set;}

		[Ordinal(11)] [RED("weaponSlot")] 		public CName WeaponSlot { get; set;}

		[Ordinal(12)] [RED("boneIndex")] 		public CInt32 BoneIndex { get; set;}

		[Ordinal(13)] [RED("soundAttackType")] 		public CName SoundAttackType { get; set;}

		[Ordinal(14)] [RED("usedZeroStaminaPerk")] 		public CBool UsedZeroStaminaPerk { get; set;}

		[Ordinal(15)] [RED("applyBuffsIfParried")] 		public CBool ApplyBuffsIfParried { get; set;}

		public W3Action_Attack(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3Action_Attack(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}