using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskDrawItem : IBehTreeTask
	{
		[Ordinal(1)] [RED("owner")] 		public CHandle<CNewNPC> Owner { get; set;}

		[Ordinal(2)] [RED("inventory")] 		public CHandle<CInventoryComponent> Inventory { get; set;}

		[Ordinal(3)] [RED("temp", 2,0)] 		public CArray<SItemUniqueId> Temp { get; set;}

		[Ordinal(4)] [RED("itemName")] 		public CName ItemName { get; set;}

		[Ordinal(5)] [RED("eventName")] 		public CName EventName { get; set;}

		public CBTTaskDrawItem(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskDrawItem(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}