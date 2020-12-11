using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SPostponedPreAttackEvent : CVariable
	{
		[Ordinal(1)] [RED("entity")] 		public CHandle<CGameplayEntity> Entity { get; set;}

		[Ordinal(2)] [RED("eventName")] 		public CName EventName { get; set;}

		[Ordinal(3)] [RED("eventType")] 		public CEnum<EAnimationEventType> EventType { get; set;}

		[Ordinal(4)] [RED("data")] 		public CPreAttackEventData Data { get; set;}

		[Ordinal(5)] [RED("animInfo")] 		public SAnimationEventAnimInfo AnimInfo { get; set;}

		public SPostponedPreAttackEvent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SPostponedPreAttackEvent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}