using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTask3StateCharge : CBTTask3StateAttack
	{
		[Ordinal(1)] [RED("differentChargeEndings")] 		public CBool DifferentChargeEndings { get; set;}

		[Ordinal(2)] [RED("bCollisionWithActor")] 		public CBool BCollisionWithActor { get; set;}

		[Ordinal(3)] [RED("loopStart")] 		public CBool LoopStart { get; set;}

		[Ordinal(4)] [RED("isEnding")] 		public CBool IsEnding { get; set;}

		[Ordinal(5)] [RED("attached")] 		public CBool Attached { get; set;}

		[Ordinal(6)] [RED("cameraIndex")] 		public CInt32 CameraIndex { get; set;}

		[Ordinal(7)] [RED("collidedActor")] 		public CHandle<CActor> CollidedActor { get; set;}

		public CBTTask3StateCharge(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTask3StateCharge(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}