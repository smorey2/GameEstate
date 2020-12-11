using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CR4CommonMainMenuBase : CR4MenuBase
	{
		[Ordinal(1)] [RED("m_menuData", 2,0)] 		public CArray<SMenuTab> M_menuData { get; set;}

		[Ordinal(2)] [RED("m_fxSetMovieData")] 		public CHandle<CScriptedFlashFunction> M_fxSetMovieData { get; set;}

		[Ordinal(3)] [RED("importSelected")] 		public CBool ImportSelected { get; set;}

		[Ordinal(4)] [RED("reopenRequested")] 		public CBool ReopenRequested { get; set;}

		[Ordinal(5)] [RED("currentMenuName")] 		public CName CurrentMenuName { get; set;}

		public CR4CommonMainMenuBase(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CR4CommonMainMenuBase(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}