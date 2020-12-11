using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CStorySceneEventChangePose : CStorySceneEventAnimClip
	{
		[Ordinal(1)] [RED("stateName")] 		public CName StateName { get; set;}

		[Ordinal(2)] [RED("status")] 		public CName Status { get; set;}

		[Ordinal(3)] [RED("emotionalState")] 		public CName EmotionalState { get; set;}

		[Ordinal(4)] [RED("poseName")] 		public CName PoseName { get; set;}

		[Ordinal(5)] [RED("transitionAnimation")] 		public CName TransitionAnimation { get; set;}

		[Ordinal(6)] [RED("useMotionExtraction")] 		public CBool UseMotionExtraction { get; set;}

		[Ordinal(7)] [RED("forceBodyIdleAnimation")] 		public CName ForceBodyIdleAnimation { get; set;}

		[Ordinal(8)] [RED("useWeightCurve")] 		public CBool UseWeightCurve { get; set;}

		[Ordinal(9)] [RED("weightCurve")] 		public SCurveData WeightCurve { get; set;}

		[Ordinal(10)] [RED("resetCloth")] 		public CEnum<EDialogResetClothAndDanglesType> ResetCloth { get; set;}

		public CStorySceneEventChangePose(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CStorySceneEventChangePose(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}