using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CGuiConfigResource : CResource
	{
		[Ordinal(1)] [RED("huds", 2,0)] 		public CArray<SHudDescription> Huds { get; set;}

		[Ordinal(2)] [RED("menus", 2,0)] 		public CArray<SMenuDescription> Menus { get; set;}

		[Ordinal(3)] [RED("popups", 2,0)] 		public CArray<SPopupDescription> Popups { get; set;}

		[Ordinal(4)] [RED("scene")] 		public SGuiSceneDescription Scene { get; set;}

		public CGuiConfigResource(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CGuiConfigResource(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}