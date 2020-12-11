using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAppearanceComponent : CComponent
	{
		[Ordinal(1)] [RED("forcedAppearance")] 		public CName ForcedAppearance { get; set;}

		[Ordinal(2)] [RED("attachmentReplacements")] 		public SAttachmentReplacements AttachmentReplacements { get; set;}

		[Ordinal(3)] [RED("appearanceAttachments", 2,0)] 		public CArray<SAppearanceAttachments> AppearanceAttachments { get; set;}

		public CAppearanceComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAppearanceComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}