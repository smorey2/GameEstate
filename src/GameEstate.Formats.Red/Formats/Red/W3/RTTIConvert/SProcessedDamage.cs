using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SProcessedDamage : CVariable
	{
		[Ordinal(1)] [RED("vitalityDamage")] 		public CFloat VitalityDamage { get; set;}

		[Ordinal(2)] [RED("essenceDamage")] 		public CFloat EssenceDamage { get; set;}

		[Ordinal(3)] [RED("moraleDamage")] 		public CFloat MoraleDamage { get; set;}

		[Ordinal(4)] [RED("staminaDamage")] 		public CFloat StaminaDamage { get; set;}

		public SProcessedDamage(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SProcessedDamage(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}