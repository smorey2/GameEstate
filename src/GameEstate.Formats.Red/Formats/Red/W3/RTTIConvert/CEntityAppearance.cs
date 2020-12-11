using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CEntityAppearance : CVariable
	{
		[Ordinal(1)] [RED("name")] 		public CName Name { get; set;}

		[Ordinal(2)] [RED("voicetag")] 		public CName Voicetag { get; set;}

		[Ordinal(3)] [RED("appearanceParams", 2,0)] 		public CArray<CPtr<CEntityTemplateParam>> AppearanceParams { get; set;}

		[Ordinal(4)] [RED("useVertexCollapse")] 		public CBool UseVertexCollapse { get; set;}

		[Ordinal(5)] [RED("usesRobe")] 		public CBool UsesRobe { get; set;}

		[Ordinal(6)] [RED("wasIncluded")] 		public CBool WasIncluded { get; set;}

		[Ordinal(7)] [RED("includedTemplates", 2,0)] 		public CArray<CHandle<CEntityTemplate>> IncludedTemplates { get; set;}

		[Ordinal(8)] [RED("collapsedComponents", 2,0)] 		public CArray<CName> CollapsedComponents { get; set;}

		public CEntityAppearance(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CEntityAppearance(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}