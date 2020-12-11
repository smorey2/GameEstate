using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeDecoratorSetBehaviorVariableDefinition : IBehTreeNodeDecoratorDefinition
	{
		[Ordinal(1)] [RED("VarName")] 		public CName VarName { get; set;}

		[Ordinal(2)] [RED("setVarActivate")] 		public CBool SetVarActivate { get; set;}

		[Ordinal(3)] [RED("valueActivate")] 		public CFloat ValueActivate { get; set;}

		[Ordinal(4)] [RED("setVarDeactivate")] 		public CBool SetVarDeactivate { get; set;}

		[Ordinal(5)] [RED("valueDeactivate")] 		public CFloat ValueDeactivate { get; set;}

		public CBehTreeDecoratorSetBehaviorVariableDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeDecoratorSetBehaviorVariableDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}