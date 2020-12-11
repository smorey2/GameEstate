using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphOutputNode : CBehaviorGraphNode
	{
		[Ordinal(1)] [RED("cachedInputNode")] 		public CPtr<CBehaviorGraphNode> CachedInputNode { get; set;}

		[Ordinal(2)] [RED("cachedCustomInputNodes", 2,0)] 		public CArray<CPtr<CBehaviorGraphValueNode>> CachedCustomInputNodes { get; set;}

		[Ordinal(3)] [RED("cachedFloatInputNodes", 2,0)] 		public CArray<CPtr<CBehaviorGraphValueNode>> CachedFloatInputNodes { get; set;}

		public CBehaviorGraphOutputNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphOutputNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}