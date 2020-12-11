using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3InventoryItemContext : W3UIContext
	{
		[Ordinal(1)] [RED("currentItemId")] 		public SItemUniqueId CurrentItemId { get; set;}

		[Ordinal(2)] [RED("currentSlot")] 		public CEnum<EEquipmentSlots> CurrentSlot { get; set;}

		[Ordinal(3)] [RED("invMenuRef")] 		public CHandle<CR4InventoryMenu> InvMenuRef { get; set;}

		[Ordinal(4)] [RED("invComponentRef")] 		public CHandle<CInventoryComponent> InvComponentRef { get; set;}

		[Ordinal(5)] [RED("invSecondComponentRef")] 		public CHandle<CInventoryComponent> InvSecondComponentRef { get; set;}

		[Ordinal(6)] [RED("contextMenuPosition_x")] 		public CFloat ContextMenuPosition_x { get; set;}

		[Ordinal(7)] [RED("contextMenuPosition_y")] 		public CFloat ContextMenuPosition_y { get; set;}

		public W3InventoryItemContext(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3InventoryItemContext(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}