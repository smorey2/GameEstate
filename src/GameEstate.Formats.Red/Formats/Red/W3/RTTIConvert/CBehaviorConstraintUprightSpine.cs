using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorConstraintUprightSpine : CBehaviorGraphPoseConstraintNode
	{
		[Ordinal(1)] [RED("bones")] 		public SBehaviorConstraintUprightSpineBonesData Bones { get; set;}

		[Ordinal(2)] [RED("leftHandIK")] 		public STwoBonesIKSolverData LeftHandIK { get; set;}

		[Ordinal(3)] [RED("rightHandIK")] 		public STwoBonesIKSolverData RightHandIK { get; set;}

		[Ordinal(4)] [RED("matchEntityFullSpeed")] 		public CFloat MatchEntityFullSpeed { get; set;}

		public CBehaviorConstraintUprightSpine(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorConstraintUprightSpine(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}