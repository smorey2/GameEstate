using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTTaskEredinSetIsAttackAvailable : IBehTreeTask
	{
		[Ordinal(1)] [RED("combatDataStorage")] 		public CHandle<CBossAICombatStorage> CombatDataStorage { get; set;}

		[Ordinal(2)] [RED("attack")] 		public CEnum<EBossSpecialAttacks> Attack { get; set;}

		[Ordinal(3)] [RED("val")] 		public CBool Val { get; set;}

		[Ordinal(4)] [RED("onActivate")] 		public CBool OnActivate { get; set;}

		public BTTaskEredinSetIsAttackAvailable(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTTaskEredinSetIsAttackAvailable(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}