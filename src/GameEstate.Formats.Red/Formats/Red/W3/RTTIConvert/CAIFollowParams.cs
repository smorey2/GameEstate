using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAIFollowParams : IAIActionParameters
	{
		[Ordinal(1)] [RED("targetTag")] 		public CName TargetTag { get; set;}

		[Ordinal(2)] [RED("moveType")] 		public CEnum<EMoveType> MoveType { get; set;}

		[Ordinal(3)] [RED("keepDistance")] 		public CBool KeepDistance { get; set;}

		[Ordinal(4)] [RED("followDistance")] 		public CFloat FollowDistance { get; set;}

		[Ordinal(5)] [RED("moveSpeed")] 		public CFloat MoveSpeed { get; set;}

		[Ordinal(6)] [RED("followTargetSelection")] 		public CBool FollowTargetSelection { get; set;}

		[Ordinal(7)] [RED("teleportToCatchup")] 		public CBool TeleportToCatchup { get; set;}

		[Ordinal(8)] [RED("cachupDistance")] 		public CFloat CachupDistance { get; set;}

		[Ordinal(9)] [RED("rotateToWhenAtTarget")] 		public CBool RotateToWhenAtTarget { get; set;}

		public CAIFollowParams(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAIFollowParams(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}