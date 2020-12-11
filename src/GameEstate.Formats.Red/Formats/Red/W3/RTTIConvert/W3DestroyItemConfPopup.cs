using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3DestroyItemConfPopup : ConfirmationPopupData
	{
		[Ordinal(1)] [RED("menuRef")] 		public CHandle<CR4InventoryMenu> MenuRef { get; set;}

		[Ordinal(2)] [RED("item")] 		public SItemUniqueId Item { get; set;}

		[Ordinal(3)] [RED("quantity")] 		public CInt32 Quantity { get; set;}

		public W3DestroyItemConfPopup(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3DestroyItemConfPopup(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}