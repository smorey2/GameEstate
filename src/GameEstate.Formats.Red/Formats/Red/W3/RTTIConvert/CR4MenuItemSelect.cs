using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4MenuItemSelect : CR4OverlayMenu
	{
		[Ordinal(1)] [RED("_itemsInv")] 		public CHandle<W3GuiItemSelectComponent> _itemsInv { get; set;}

		[Ordinal(2)] [RED("_invComponent")] 		public CHandle<CInventoryComponent> _invComponent { get; set;}

		[Ordinal(3)] [RED("_initData")] 		public CHandle<W3ItemSelectMenuInitData> _initData { get; set;}

		public CR4MenuItemSelect(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4MenuItemSelect(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}