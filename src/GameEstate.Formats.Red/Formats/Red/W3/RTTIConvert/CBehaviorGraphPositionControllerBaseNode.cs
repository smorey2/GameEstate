using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphPositionControllerBaseNode : CBehaviorGraphBaseNode
	{
		[Ordinal(1)] [RED("useHeading")] 		public CBool UseHeading { get; set;}

		[Ordinal(2)] [RED("cachedWeightVariableNode")] 		public CPtr<CBehaviorGraphValueNode> CachedWeightVariableNode { get; set;}

		[Ordinal(3)] [RED("cachedShiftVariableNode")] 		public CPtr<CBehaviorGraphVectorValueNode> CachedShiftVariableNode { get; set;}

		public CBehaviorGraphPositionControllerBaseNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphPositionControllerBaseNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}