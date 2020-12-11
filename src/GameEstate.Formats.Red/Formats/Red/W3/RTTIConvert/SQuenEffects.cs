using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SQuenEffects : CVariable
	{
		[Ordinal(1)] [RED("lastingEffectUpgNone")] 		public CName LastingEffectUpgNone { get; set;}

		[Ordinal(2)] [RED("lastingEffectUpg1")] 		public CName LastingEffectUpg1 { get; set;}

		[Ordinal(3)] [RED("lastingEffectUpg2")] 		public CName LastingEffectUpg2 { get; set;}

		[Ordinal(4)] [RED("lastingEffectUpg3")] 		public CName LastingEffectUpg3 { get; set;}

		[Ordinal(5)] [RED("castEffect")] 		public CName CastEffect { get; set;}

		[Ordinal(6)] [RED("cameraShakeStranth")] 		public CFloat CameraShakeStranth { get; set;}

		public SQuenEffects(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SQuenEffects(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}