using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorConstraintNodeFloorIKBase : CBehaviorGraphPoseConstraintNode
	{
		[Ordinal(1)] [RED("requiredAnimEvent")] 		public CName RequiredAnimEvent { get; set;}

		[Ordinal(2)] [RED("blockAnimEvent")] 		public CName BlockAnimEvent { get; set;}

		[Ordinal(3)] [RED("canBeDisabledDueToFrameRate")] 		public CBool CanBeDisabledDueToFrameRate { get; set;}

		[Ordinal(4)] [RED("useFixedVersion")] 		public CBool UseFixedVersion { get; set;}

		[Ordinal(5)] [RED("slopeAngleDamp")] 		public CFloat SlopeAngleDamp { get; set;}

		[Ordinal(6)] [RED("generateEditorFragmentsForIKSolvers")] 		public CBool GenerateEditorFragmentsForIKSolvers { get; set;}

		[Ordinal(7)] [RED("generateEditorFragmentsForLegIndex")] 		public CInt32 GenerateEditorFragmentsForLegIndex { get; set;}

		[Ordinal(8)] [RED("common")] 		public SBehaviorConstraintNodeFloorIKCommonData Common { get; set;}

		public CBehaviorConstraintNodeFloorIKBase(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorConstraintNodeFloorIKBase(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}