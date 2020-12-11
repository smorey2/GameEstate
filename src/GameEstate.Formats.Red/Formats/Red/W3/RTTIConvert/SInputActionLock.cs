using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SInputActionLock : CVariable
	{
		[Ordinal(1)] [RED("sourceName")] 		public CName SourceName { get; set;}

		[Ordinal(2)] [RED("removedOnSpawn")] 		public CBool RemovedOnSpawn { get; set;}

		[Ordinal(3)] [RED("isFromQuest")] 		public CBool IsFromQuest { get; set;}

		[Ordinal(4)] [RED("isFromPlace")] 		public CBool IsFromPlace { get; set;}

		public SInputActionLock(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SInputActionLock(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}