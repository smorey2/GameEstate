using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphMimicGainNode : CBehaviorGraphBaseMimicNode
	{
		[Ordinal(1)] [RED("gain")] 		public CFloat Gain { get; set;}

		[Ordinal(2)] [RED("min")] 		public CFloat Min { get; set;}

		[Ordinal(3)] [RED("max")] 		public CFloat Max { get; set;}

		[Ordinal(4)] [RED("cachedGainValueNode")] 		public CPtr<CBehaviorGraphValueNode> CachedGainValueNode { get; set;}

		public CBehaviorGraphMimicGainNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphMimicGainNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}