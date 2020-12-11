using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3DeleteSaveConf : ConfirmationPopupData
	{
		[Ordinal(1)] [RED("menuRef")] 		public CHandle<CR4IngameMenu> MenuRef { get; set;}

		[Ordinal(2)] [RED("type")] 		public CEnum<ESaveGameType> Type { get; set;}

		[Ordinal(3)] [RED("slot")] 		public CInt32 Slot { get; set;}

		[Ordinal(4)] [RED("saveMode")] 		public CBool SaveMode { get; set;}

		public W3DeleteSaveConf(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3DeleteSaveConf(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}