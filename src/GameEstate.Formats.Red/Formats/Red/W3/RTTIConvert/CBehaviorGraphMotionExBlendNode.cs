using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphMotionExBlendNode : CBehaviorGraphNode
	{
		[Ordinal(1)] [RED("cachedFirstInputNode")] 		public CPtr<CBehaviorGraphNode> CachedFirstInputNode { get; set;}

		[Ordinal(2)] [RED("cachedSecondInputNode")] 		public CPtr<CBehaviorGraphNode> CachedSecondInputNode { get; set;}

		[Ordinal(3)] [RED("cachedControlVariableNode")] 		public CPtr<CBehaviorGraphValueNode> CachedControlVariableNode { get; set;}

		[Ordinal(4)] [RED("cachedSpeedVariableNode")] 		public CPtr<CBehaviorGraphValueNode> CachedSpeedVariableNode { get; set;}

		[Ordinal(5)] [RED("threshold")] 		public CFloat Threshold { get; set;}

		public CBehaviorGraphMotionExBlendNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphMotionExBlendNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}