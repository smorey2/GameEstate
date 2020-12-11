using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphMimicMathOpNode : CBehaviorGraphBaseMimicNode
	{
		[Ordinal(1)] [RED("cachedValueNode")] 		public CPtr<CBehaviorGraphValueNode> CachedValueNode { get; set;}

		[Ordinal(2)] [RED("mathOp")] 		public CEnum<EBehaviorMimicMathOp> MathOp { get; set;}

		[Ordinal(3)] [RED("value")] 		public CFloat Value { get; set;}

		[Ordinal(4)] [RED("trackName")] 		public CString TrackName { get; set;}

		public CBehaviorGraphMimicMathOpNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphMimicMathOpNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}