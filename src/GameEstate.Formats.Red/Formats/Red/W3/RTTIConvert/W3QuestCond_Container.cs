using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3QuestCond_Container : CQuestScriptedCondition
	{
		[Ordinal(1)] [RED("containerTag")] 		public CName ContainerTag { get; set;}

		[Ordinal(2)] [RED("contents")] 		public CEnum<EContainerMode> Contents { get; set;}

		[Ordinal(3)] [RED("inventory")] 		public CHandle<CInventoryComponent> Inventory { get; set;}

		[Ordinal(4)] [RED("isFulfilled")] 		public CBool IsFulfilled { get; set;}

		[Ordinal(5)] [RED("globalListener")] 		public CHandle<W3QuestCond_Container_GlobalListener> GlobalListener { get; set;}

		[Ordinal(6)] [RED("inventoryListener")] 		public CHandle<W3QuestCond_Container_InventoryListener> InventoryListener { get; set;}

		public W3QuestCond_Container(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3QuestCond_Container(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}