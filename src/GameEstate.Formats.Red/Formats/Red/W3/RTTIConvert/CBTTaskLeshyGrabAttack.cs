using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskLeshyGrabAttack : IBehTreeTask
	{
		[Ordinal(1)] [RED("attackType")] 		public CEnum<EAttackType> AttackType { get; set;}

		[Ordinal(2)] [RED("stopTaskAfterDealingDmg")] 		public CBool StopTaskAfterDealingDmg { get; set;}

		[Ordinal(3)] [RED("useDirectionalAttacks")] 		public CBool UseDirectionalAttacks { get; set;}

		[Ordinal(4)] [RED("fxOnDamageInstigated")] 		public CName FxOnDamageInstigated { get; set;}

		[Ordinal(5)] [RED("slave")] 		public CHandle<CActor> Slave { get; set;}

		[Ordinal(6)] [RED("slaveComponent")] 		public CHandle<CEffectDummyComponent> SlaveComponent { get; set;}

		public CBTTaskLeshyGrabAttack(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskLeshyGrabAttack(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}