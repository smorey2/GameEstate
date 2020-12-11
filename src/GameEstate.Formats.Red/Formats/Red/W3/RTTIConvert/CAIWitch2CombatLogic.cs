using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAIWitch2CombatLogic : CAIMonsterCombatLogic
	{
		[Ordinal(1)] [RED("Phase1")] 		public CBool Phase1 { get; set;}

		[Ordinal(2)] [RED("Phase2")] 		public CBool Phase2 { get; set;}

		[Ordinal(3)] [RED("PhaseReset")] 		public CBool PhaseReset { get; set;}

		[Ordinal(4)] [RED("bileAttack")] 		public CBool BileAttack { get; set;}

		[Ordinal(5)] [RED("prePursueTaunt")] 		public CBool PrePursueTaunt { get; set;}

		public CAIWitch2CombatLogic(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAIWitch2CombatLogic(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}