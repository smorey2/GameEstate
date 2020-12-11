using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CExtAnimItemEvent : CExtAnimEvent
	{
		[Ordinal(1)] [RED("category")] 		public CName Category { get; set;}

		[Ordinal(2)] [RED("itemName_optional")] 		public CName ItemName_optional { get; set;}

		[Ordinal(3)] [RED("action")] 		public CEnum<EItemAction> Action { get; set;}

		[Ordinal(4)] [RED("ignoreItemsWithTag")] 		public CName IgnoreItemsWithTag { get; set;}

		[Ordinal(5)] [RED("itemGetting")] 		public CEnum<EGettingItem> ItemGetting { get; set;}

		public CExtAnimItemEvent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CExtAnimItemEvent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}