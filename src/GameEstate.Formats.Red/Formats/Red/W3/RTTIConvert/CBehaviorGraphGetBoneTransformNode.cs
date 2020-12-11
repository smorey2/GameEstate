using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphGetBoneTransformNode : CBehaviorGraphVectorValueNode
	{
		[Ordinal(1)] [RED("boneName")] 		public CString BoneName { get; set;}

		[Ordinal(2)] [RED("type")] 		public CEnum<ETransformType> Type { get; set;}

		[Ordinal(3)] [RED("cachedInputNode")] 		public CPtr<CBehaviorGraphNode> CachedInputNode { get; set;}

		public CBehaviorGraphGetBoneTransformNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphGetBoneTransformNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}