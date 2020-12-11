using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CTicketBaseAlgorithm : ITicketAlgorithmScript
	{
		[Ordinal(1)] [RED("resetImportanceOnSpecialCombatAction")] 		public CBool ResetImportanceOnSpecialCombatAction { get; set;}

		[Ordinal(2)] [RED("threatLevelBonus")] 		public CFloat ThreatLevelBonus { get; set;}

		[Ordinal(3)] [RED("activationBonus")] 		public CFloat ActivationBonus { get; set;}

		public CTicketBaseAlgorithm(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CTicketBaseAlgorithm(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}