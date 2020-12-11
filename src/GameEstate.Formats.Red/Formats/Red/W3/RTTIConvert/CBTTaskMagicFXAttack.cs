using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskMagicFXAttack : CBTTaskMagicAttack
	{
		[Ordinal(1)] [RED("resourceName")] 		public CName ResourceName { get; set;}

		[Ordinal(2)] [RED("effectEntityTemplate")] 		public CHandle<CEntityTemplate> EffectEntityTemplate { get; set;}

		[Ordinal(3)] [RED("dealDmgOnDeactivate")] 		public CBool DealDmgOnDeactivate { get; set;}

		[Ordinal(4)] [RED("couldntLoadResource")] 		public CBool CouldntLoadResource { get; set;}

		public CBTTaskMagicFXAttack(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskMagicFXAttack(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}