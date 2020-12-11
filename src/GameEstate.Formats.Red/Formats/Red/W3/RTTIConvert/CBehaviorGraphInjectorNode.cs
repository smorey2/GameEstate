using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphInjectorNode : CBehaviorGraphBaseNode
	{
		[Ordinal(1)] [RED("cachedInjectorNode")] 		public CPtr<CBehaviorGraphNode> CachedInjectorNode { get; set;}

		[Ordinal(2)] [RED("cachedControlNode")] 		public CPtr<CBehaviorGraphValueNode> CachedControlNode { get; set;}

		public CBehaviorGraphInjectorNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphInjectorNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}