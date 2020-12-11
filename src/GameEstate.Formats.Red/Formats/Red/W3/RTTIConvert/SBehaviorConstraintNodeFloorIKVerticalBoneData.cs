using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SBehaviorConstraintNodeFloorIKVerticalBoneData : CVariable
	{
		[Ordinal(1)] [RED("bone")] 		public CName Bone { get; set;}

		[Ordinal(2)] [RED("Min offset")] 		public CFloat Min_offset { get; set;}

		[Ordinal(3)] [RED("Max offset")] 		public CFloat Max_offset { get; set;}

		[Ordinal(4)] [RED("offsetToDesiredBlendTime")] 		public CFloat OffsetToDesiredBlendTime { get; set;}

		[Ordinal(5)] [RED("verticalOffsetBlendTime")] 		public CFloat VerticalOffsetBlendTime { get; set;}

		[Ordinal(6)] [RED("stiffness")] 		public CFloat Stiffness { get; set;}

		public SBehaviorConstraintNodeFloorIKVerticalBoneData(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SBehaviorConstraintNodeFloorIKVerticalBoneData(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}