using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CQuestEntityMotionBlock : CQuestGraphBlock
	{
		[Ordinal(1)] [RED("entityTag")] 		public CName EntityTag { get; set;}

		[Ordinal(2)] [RED("duration")] 		public CFloat Duration { get; set;}

		[Ordinal(3)] [RED("targetTransform")] 		public EngineTransform TargetTransform { get; set;}

		[Ordinal(4)] [RED("positionDelta")] 		public Vector PositionDelta { get; set;}

		[Ordinal(5)] [RED("rotationDelta")] 		public EulerAngles RotationDelta { get; set;}

		[Ordinal(6)] [RED("scaleDelta")] 		public Vector ScaleDelta { get; set;}

		[Ordinal(7)] [RED("animationCurve")] 		public SSimpleCurve AnimationCurve { get; set;}

		public CQuestEntityMotionBlock(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CQuestEntityMotionBlock(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}