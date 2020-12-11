using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SSavegameInfo : CVariable
	{
		[Ordinal(1)] [RED("filename")] 		public CString Filename { get; set;}

		[Ordinal(2)] [RED("slotType")] 		public CEnum<ESaveGameType> SlotType { get; set;}

		[Ordinal(3)] [RED("slotIndex")] 		public CInt32 SlotIndex { get; set;}

		public SSavegameInfo(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SSavegameInfo(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}