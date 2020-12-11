using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorConstraintRiderInSaddle : CBehaviorGraphPoseConstraintNode
	{
		[Ordinal(1)] [RED("bone")] 		public CName Bone { get; set;}

		[Ordinal(2)] [RED("blendTime")] 		public CFloat BlendTime { get; set;}

		[Ordinal(3)] [RED("blendRotation")] 		public CFloat BlendRotation { get; set;}

		public CBehaviorConstraintRiderInSaddle(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorConstraintRiderInSaddle(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}