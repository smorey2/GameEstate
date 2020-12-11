using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphSetInternalVariableNode : CBehaviorGraphBaseNode
	{
		[Ordinal(1)] [RED("variableName")] 		public CName VariableName { get; set;}

		[Ordinal(2)] [RED("setValueOnActivationAsWell")] 		public CBool SetValueOnActivationAsWell { get; set;}

		[Ordinal(3)] [RED("setValueBeforeInputIsUpdated")] 		public CBool SetValueBeforeInputIsUpdated { get; set;}

		[Ordinal(4)] [RED("cachedValueNode")] 		public CPtr<CBehaviorGraphValueNode> CachedValueNode { get; set;}

		public CBehaviorGraphSetInternalVariableNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphSetInternalVariableNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}