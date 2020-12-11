using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class IModUiEditableListCallback : IModUiMenuCallback
	{
		[Ordinal(1)] [RED("listMenuRef")] 		public CHandle<CModUiEditableListView> ListMenuRef { get; set;}

		[Ordinal(2)] [RED("title")] 		public CString Title { get; set;}

		[Ordinal(3)] [RED("statsLabel")] 		public CString StatsLabel { get; set;}

		[Ordinal(4)] [RED("listData", 2,0)] 		public CArray<SModUiListItem> ListData { get; set;}

		public IModUiEditableListCallback(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new IModUiEditableListCallback(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}