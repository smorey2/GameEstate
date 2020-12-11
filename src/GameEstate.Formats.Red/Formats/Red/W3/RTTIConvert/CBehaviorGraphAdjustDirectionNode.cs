using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphAdjustDirectionNode : CBehaviorGraphBaseNode
	{
		[Ordinal(1)] [RED("animDirectionChange")] 		public CFloat AnimDirectionChange { get; set;}

		[Ordinal(2)] [RED("updateAnimDirectionChangeFromAnimation")] 		public CBool UpdateAnimDirectionChangeFromAnimation { get; set;}

		[Ordinal(3)] [RED("maxDirectionDiff")] 		public CFloat MaxDirectionDiff { get; set;}

		[Ordinal(4)] [RED("maxOppositeDirectionDiff")] 		public CFloat MaxOppositeDirectionDiff { get; set;}

		[Ordinal(5)] [RED("basedOnEvent")] 		public CName BasedOnEvent { get; set;}

		[Ordinal(6)] [RED("basedOnEventOverrideAnimation")] 		public CBool BasedOnEventOverrideAnimation { get; set;}

		[Ordinal(7)] [RED("adjustmentBlendSpeed")] 		public CFloat AdjustmentBlendSpeed { get; set;}

		[Ordinal(8)] [RED("requestedMovementDirectionVariableName")] 		public CName RequestedMovementDirectionVariableName { get; set;}

		[Ordinal(9)] [RED("cachedRequestedMovementDirectionWSValueNode")] 		public CPtr<CBehaviorGraphValueNode> CachedRequestedMovementDirectionWSValueNode { get; set;}

		public CBehaviorGraphAdjustDirectionNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphAdjustDirectionNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}