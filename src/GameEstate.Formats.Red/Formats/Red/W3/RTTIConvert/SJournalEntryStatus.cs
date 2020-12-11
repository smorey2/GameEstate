using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SJournalEntryStatus : CVariable
	{
		[Ordinal(1)] [RED("entry")] 		public CPtr<CJournalBase> Entry { get; set;}

		[Ordinal(2)] [RED("status")] 		public CEnum<EJournalStatus> Status { get; set;}

		[Ordinal(3)] [RED("unread")] 		public CBool Unread { get; set;}

		public SJournalEntryStatus(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SJournalEntryStatus(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}