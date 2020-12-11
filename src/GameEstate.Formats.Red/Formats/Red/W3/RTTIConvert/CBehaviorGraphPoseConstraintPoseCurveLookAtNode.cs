using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphPoseConstraintPoseCurveLookAtNode : CBehaviorGraphPoseConstraintPoseLookAtNode
	{
		[Ordinal(1)] [RED("useCurve")] 		public CBool UseCurve { get; set;}

		[Ordinal(2)] [RED("curve")] 		public CPtr<CCurve> Curve { get; set;}

		[Ordinal(3)] [RED("doubleDamp")] 		public CBool DoubleDamp { get; set;}

		public CBehaviorGraphPoseConstraintPoseCurveLookAtNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphPoseConstraintPoseCurveLookAtNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}