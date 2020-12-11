using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskHarpyDustAttack : CBTTaskAttack
	{
		[Ordinal(1)] [RED("addtionalFX")] 		public CName AddtionalFX { get; set;}

		[Ordinal(2)] [RED("effectRange")] 		public CFloat EffectRange { get; set;}

		[Ordinal(3)] [RED("effectAngle")] 		public CFloat EffectAngle { get; set;}

		[Ordinal(4)] [RED("eventReceived")] 		public CBool EventReceived { get; set;}

		public CBTTaskHarpyDustAttack(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskHarpyDustAttack(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}