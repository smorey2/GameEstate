using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphVectorMathNode : CBehaviorGraphVectorValueNode
	{
		[Ordinal(1)] [RED("operation")] 		public CEnum<EBehaviorVectorMathOp> Operation { get; set;}

		[Ordinal(2)] [RED("cachedFirstInputNode")] 		public CPtr<CBehaviorGraphVectorValueNode> CachedFirstInputNode { get; set;}

		[Ordinal(3)] [RED("cachedSecondInputNode")] 		public CPtr<CBehaviorGraphVectorValueNode> CachedSecondInputNode { get; set;}

		[Ordinal(4)] [RED("cachedScalarInputNode")] 		public CPtr<CBehaviorGraphValueNode> CachedScalarInputNode { get; set;}

		public CBehaviorGraphVectorMathNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphVectorMathNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}