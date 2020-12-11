using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeCombatStyleManager : IBehTreeTask
	{
		[Ordinal(1)] [RED("combatDataStorage")] 		public CHandle<CHumanAICombatStorage> CombatDataStorage { get; set;}

		[Ordinal(2)] [RED("preferedCombatStyle")] 		public CEnum<EBehaviorGraph> PreferedCombatStyle { get; set;}

		[Ordinal(3)] [RED("isRanged")] 		public CBool IsRanged { get; set;}

		[Ordinal(4)] [RED("rangedWeaponType")] 		public CName RangedWeaponType { get; set;}

		public CBehTreeCombatStyleManager(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeCombatStyleManager(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}