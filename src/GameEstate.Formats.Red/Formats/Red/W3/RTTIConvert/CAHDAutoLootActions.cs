using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAHDAutoLootActions : CObject
	{
		[Ordinal(1)] [RED("AutoLootConfig")] 		public CHandle<CAHDAutoLootConfig> AutoLootConfig { get; set;}

		[Ordinal(2)] [RED("AutoLootNotificationManager")] 		public CHandle<CAHDAutoLootNotificationManager> AutoLootNotificationManager { get; set;}

		[Ordinal(3)] [RED("cInv")] 		public CHandle<CInventoryComponent> CInv { get; set;}

		[Ordinal(4)] [RED("invItemList", 2,0)] 		public CArray<SItemUniqueId> InvItemList { get; set;}

		public CAHDAutoLootActions(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAHDAutoLootActions(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}