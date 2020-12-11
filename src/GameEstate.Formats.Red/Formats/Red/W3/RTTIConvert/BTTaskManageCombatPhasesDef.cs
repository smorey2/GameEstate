using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTTaskManageCombatPhasesDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("rangedCombatPhaseParameters")] 		public SCombatPhaseParameters RangedCombatPhaseParameters { get; set;}

		[Ordinal(2)] [RED("closeCombatPhaseParameters")] 		public SCombatPhaseParameters CloseCombatPhaseParameters { get; set;}

		[Ordinal(3)] [RED("nonCombatPhaseParameters")] 		public SCombatPhaseParameters NonCombatPhaseParameters { get; set;}

		[Ordinal(4)] [RED("setBehVariableName")] 		public CName SetBehVariableName { get; set;}

		public BTTaskManageCombatPhasesDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTTaskManageCombatPhasesDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}