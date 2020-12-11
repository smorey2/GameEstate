using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphFillMovementVariablesUsingSteeringNode : CBehaviorGraphBaseNode
	{
		[Ordinal(1)] [RED("fillRequestedMovementDirectionWSVariable")] 		public CBool FillRequestedMovementDirectionWSVariable { get; set;}

		[Ordinal(2)] [RED("requestedMovementDirectionWSVariableName")] 		public CName RequestedMovementDirectionWSVariableName { get; set;}

		[Ordinal(3)] [RED("fillRequestedFacingDirectionWSVariable")] 		public CBool FillRequestedFacingDirectionWSVariable { get; set;}

		[Ordinal(4)] [RED("requestedFacingDirectionWSVariableName")] 		public CName RequestedFacingDirectionWSVariableName { get; set;}

		public CBehaviorGraphFillMovementVariablesUsingSteeringNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphFillMovementVariablesUsingSteeringNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}