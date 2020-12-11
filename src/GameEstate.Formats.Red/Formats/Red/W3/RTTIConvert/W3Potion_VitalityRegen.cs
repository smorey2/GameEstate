using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3Potion_VitalityRegen : W3RegenEffect
	{
		[Ordinal(1)] [RED("combatRegen")] 		public SAbilityAttributeValue CombatRegen { get; set;}

		[Ordinal(2)] [RED("nonCombatRegen")] 		public SAbilityAttributeValue NonCombatRegen { get; set;}

		[Ordinal(3)] [RED("playerTarget")] 		public CHandle<CR4Player> PlayerTarget { get; set;}

		public W3Potion_VitalityRegen(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3Potion_VitalityRegen(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}