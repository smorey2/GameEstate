using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class FocusModeCameraShotHelper : CObject
	{
		[Ordinal(1)] [RED("headingOffset")] 		public CFloat HeadingOffset { get; set;}

		[Ordinal(2)] [RED("e")] 		public CHandle<CNewNPC> E { get; set;}

		[Ordinal(3)] [RED("p")] 		public CHandle<CPlayer> P { get; set;}

		[Ordinal(4)] [RED("initShot_yaw")] 		public CFloat InitShot_yaw { get; set;}

		[Ordinal(5)] [RED("initShot_yawAlt")] 		public CFloat InitShot_yawAlt { get; set;}

		[Ordinal(6)] [RED("initShot_yawA")] 		public CFloat InitShot_yawA { get; set;}

		[Ordinal(7)] [RED("initShot_yawB")] 		public CFloat InitShot_yawB { get; set;}

		[Ordinal(8)] [RED("initShot_yawC")] 		public CFloat InitShot_yawC { get; set;}

		[Ordinal(9)] [RED("initShot_yawD")] 		public CFloat InitShot_yawD { get; set;}

		[Ordinal(10)] [RED("initShot_number")] 		public CInt32 InitShot_number { get; set;}

		[Ordinal(11)] [RED("initShot_isPlayerMainChar")] 		public CBool InitShot_isPlayerMainChar { get; set;}

		[Ordinal(12)] [RED("initShot_mainCharacter")] 		public CHandle<CActor> InitShot_mainCharacter { get; set;}

		[Ordinal(13)] [RED("initShot_secCharacter")] 		public CHandle<CActor> InitShot_secCharacter { get; set;}

		[Ordinal(14)] [RED("initShot_cameraSecSide")] 		public CBool InitShot_cameraSecSide { get; set;}

		[Ordinal(15)] [RED("ssShot_yaw")] 		public CFloat SsShot_yaw { get; set;}

		[Ordinal(16)] [RED("ssShot_pitch")] 		public CFloat SsShot_pitch { get; set;}

		[Ordinal(17)] [RED("ssShot_distance")] 		public CFloat SsShot_distance { get; set;}

		[Ordinal(18)] [RED("ssShot_pivot")] 		public Vector SsShot_pivot { get; set;}

		[Ordinal(19)] [RED("blendShot_started")] 		public CBool BlendShot_started { get; set;}

		[Ordinal(20)] [RED("blendShot_duration")] 		public CFloat BlendShot_duration { get; set;}

		[Ordinal(21)] [RED("blendShot_timer")] 		public CFloat BlendShot_timer { get; set;}

		[Ordinal(22)] [RED("blendShot_progress")] 		public CFloat BlendShot_progress { get; set;}

		public FocusModeCameraShotHelper(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new FocusModeCameraShotHelper(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}