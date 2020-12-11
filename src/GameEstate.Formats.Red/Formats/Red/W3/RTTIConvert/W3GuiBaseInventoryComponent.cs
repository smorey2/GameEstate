using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3GuiBaseInventoryComponent : CObject
	{
		[Ordinal(1)] [RED("autoCleanNewMark")] 		public CBool AutoCleanNewMark { get; set;}

		[Ordinal(2)] [RED("_inv")] 		public CHandle<CInventoryComponent> _inv { get; set;}

		[Ordinal(3)] [RED("highlightedItems", 2,0)] 		public CArray<CName> HighlightedItems { get; set;}

		[Ordinal(4)] [RED("ITEM_NEED_REPAIR_DISPLAY_VALUE")] 		public CInt32 ITEM_NEED_REPAIR_DISPLAY_VALUE { get; set;}

		public W3GuiBaseInventoryComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3GuiBaseInventoryComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}