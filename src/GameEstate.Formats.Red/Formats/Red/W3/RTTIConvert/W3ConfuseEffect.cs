using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3ConfuseEffect : W3CriticalEffect
	{
		[Ordinal(1)] [RED("drainStaminaOnExit")] 		public CBool DrainStaminaOnExit { get; set;}

		[Ordinal(2)] [RED("criticalHitBonus")] 		public CFloat CriticalHitBonus { get; set;}

		public W3ConfuseEffect(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3ConfuseEffect(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}