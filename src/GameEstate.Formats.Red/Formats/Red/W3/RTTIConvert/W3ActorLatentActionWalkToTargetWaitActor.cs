using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3ActorLatentActionWalkToTargetWaitActor : IPresetActorLatentAction
	{
		[Ordinal(1)] [RED("tag")] 		public CName Tag { get; set;}

		[Ordinal(2)] [RED("maxDistance")] 		public CFloat MaxDistance { get; set;}

		[Ordinal(3)] [RED("moveSpeed")] 		public CFloat MoveSpeed { get; set;}

		[Ordinal(4)] [RED("moveType")] 		public CEnum<EMoveType> MoveType { get; set;}

		[Ordinal(5)] [RED("waitForTag")] 		public CName WaitForTag { get; set;}

		[Ordinal(6)] [RED("timeout")] 		public CFloat Timeout { get; set;}

		[Ordinal(7)] [RED("testDistance")] 		public CFloat TestDistance { get; set;}

		public W3ActorLatentActionWalkToTargetWaitActor(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3ActorLatentActionWalkToTargetWaitActor(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}