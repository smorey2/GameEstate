using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehaviorGraphInternalVariableCounterNode : CBehaviorGraphBaseNode
	{
		[Ordinal(1)] [RED("variableName")] 		public CName VariableName { get; set;}

		[Ordinal(2)] [RED("countOnActivation")] 		public CBool CountOnActivation { get; set;}

		[Ordinal(3)] [RED("countOnDeactivation")] 		public CBool CountOnDeactivation { get; set;}

		[Ordinal(4)] [RED("stepValue")] 		public CFloat StepValue { get; set;}

		public CBehaviorGraphInternalVariableCounterNode(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehaviorGraphInternalVariableCounterNode(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}