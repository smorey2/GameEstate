using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTCondActorInDanger : IBehTreeTask
	{
		[Ordinal(1)] [RED("ignoreEntityWithTag")] 		public CName IgnoreEntityWithTag { get; set;}

		[Ordinal(2)] [RED("dangerRadius")] 		public CFloat DangerRadius { get; set;}

		[Ordinal(3)] [RED("callFromQuest")] 		public CBool CallFromQuest { get; set;}

		[Ordinal(4)] [RED("checkQuestRequests")] 		public CBool CheckQuestRequests { get; set;}

		public CBTCondActorInDanger(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTCondActorInDanger(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}