using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CMenuResource : IGuiResource
	{
		[Ordinal(1)] [RED("menuClass")] 		public CName MenuClass { get; set;}

		[Ordinal(2)] [RED("menuFlashSwf")] 		public CSoft<CSwfResource> MenuFlashSwf { get; set;}

		[Ordinal(3)] [RED("layer")] 		public CUInt32 Layer { get; set;}

		[Ordinal(4)] [RED("menuDef")] 		public CPtr<CMenuDef> MenuDef { get; set;}

		public CMenuResource(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CMenuResource(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}