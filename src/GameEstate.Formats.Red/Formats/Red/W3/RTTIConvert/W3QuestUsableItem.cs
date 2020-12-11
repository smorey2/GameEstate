using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3QuestUsableItem : W3UsableItem
	{
		[Ordinal(1)] [RED("factAddedOnUse")] 		public CString FactAddedOnUse { get; set;}

		[Ordinal(2)] [RED("factValue")] 		public CInt32 FactValue { get; set;}

		[Ordinal(3)] [RED("factTimeValid")] 		public CInt32 FactTimeValid { get; set;}

		[Ordinal(4)] [RED("removeFactOnHide")] 		public CBool RemoveFactOnHide { get; set;}

		public W3QuestUsableItem(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3QuestUsableItem(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}