using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SLookAtAnimationPairDefinition : CVariable
	{
		[Ordinal(1)] [RED("Animation name")] 		public CName Animation_name { get; set;}

		[Ordinal(2)] [RED("Horizontal anim (additive)")] 		public CName Horizontal_anim__additive_ { get; set;}

		[Ordinal(3)] [RED("Vertical anim (additive)")] 		public CName Vertical_anim__additive_ { get; set;}

		public SLookAtAnimationPairDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SLookAtAnimationPairDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}