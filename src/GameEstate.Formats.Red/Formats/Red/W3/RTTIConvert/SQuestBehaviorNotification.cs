using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SQuestBehaviorNotification : CObject
	{
		[Ordinal(1)] [RED("npcTag")] 		public CName NpcTag { get; set;}

		[Ordinal(2)] [RED("notification")] 		public CName Notification { get; set;}

		[Ordinal(3)] [RED("all")] 		public CBool All { get; set;}

		public SQuestBehaviorNotification(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SQuestBehaviorNotification(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}