using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskCustomApplyEffectAttack : CBTTaskAttack
	{
		[Ordinal(1)] [RED("applyEffectInterval")] 		public CFloat ApplyEffectInterval { get; set;}

		[Ordinal(2)] [RED("activateOnAnimEvent")] 		public CName ActivateOnAnimEvent { get; set;}

		[Ordinal(3)] [RED("activationTimeStamp")] 		public CFloat ActivationTimeStamp { get; set;}

		[Ordinal(4)] [RED("activated")] 		public CBool Activated { get; set;}

		public CBTTaskCustomApplyEffectAttack(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskCustomApplyEffectAttack(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}