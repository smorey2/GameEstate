using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphAimingWithIKNode : CBehaviorGraphNode
	{
		[Ordinal(1)] [RED("aimingBaseBoneName")] 		public CName AimingBaseBoneName { get; set;}

		[Ordinal(2)] [RED("ik")] 		public STwoBonesIKSolverData Ik { get; set;}

		[Ordinal(3)] [RED("cachedInputNode")] 		public CPtr<CBehaviorGraphNode> CachedInputNode { get; set;}

		[Ordinal(4)] [RED("cachedBaseInputNode")] 		public CPtr<CBehaviorGraphNode> CachedBaseInputNode { get; set;}

		[Ordinal(5)] [RED("cachedLookAtTargetDirMSInputNode")] 		public CPtr<CBehaviorGraphVectorValueNode> CachedLookAtTargetDirMSInputNode { get; set;}

		public CBehaviorGraphAimingWithIKNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphAimingWithIKNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}