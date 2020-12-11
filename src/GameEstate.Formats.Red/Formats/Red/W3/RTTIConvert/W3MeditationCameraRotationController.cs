using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3MeditationCameraRotationController : ICustomCameraScriptedPivotRotationController
	{
		[Ordinal(1)] [RED("fixedPitch")] 		public CFloat FixedPitch { get; set;}

		[Ordinal(2)] [RED("fixedYaw")] 		public CFloat FixedYaw { get; set;}

		[Ordinal(3)] [RED("fixedRoll")] 		public CFloat FixedRoll { get; set;}

		[Ordinal(4)] [RED("baseSmooth")] 		public CFloat BaseSmooth { get; set;}

		[Ordinal(5)] [RED("desiredYaw")] 		public CFloat DesiredYaw { get; set;}

		[Ordinal(6)] [RED("desired")] 		public CBool Desired { get; set;}

		[Ordinal(7)] [RED("smooth")] 		public CFloat Smooth { get; set;}

		public W3MeditationCameraRotationController(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3MeditationCameraRotationController(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}