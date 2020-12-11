using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class ItemInfoPopupData : TextPopupData
	{
		[Ordinal(1)] [RED("invRef")] 		public CHandle<CInventoryComponent> InvRef { get; set;}

		[Ordinal(2)] [RED("itemId")] 		public SItemUniqueId ItemId { get; set;}

		[Ordinal(3)] [RED("inventoryRef")] 		public CHandle<CR4InventoryMenu> InventoryRef { get; set;}

		[Ordinal(4)] [RED("invComponent")] 		public CHandle<CInventoryComponent> InvComponent { get; set;}

		public ItemInfoPopupData(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new ItemInfoPopupData(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}