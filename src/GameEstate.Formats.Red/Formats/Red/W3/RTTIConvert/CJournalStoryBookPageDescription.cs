using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CJournalStoryBookPageDescription : CJournalContainerEntry
	{
		[Ordinal(1)] [RED("videoFilename")] 		public CString VideoFilename { get; set;}

		[Ordinal(2)] [RED("description")] 		public LocalizedString Description { get; set;}

		[Ordinal(3)] [RED("isFinal")] 		public CBool IsFinal { get; set;}

		[Ordinal(4)] [RED("active")] 		public CBool Active { get; set;}

		public CJournalStoryBookPageDescription(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CJournalStoryBookPageDescription(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}