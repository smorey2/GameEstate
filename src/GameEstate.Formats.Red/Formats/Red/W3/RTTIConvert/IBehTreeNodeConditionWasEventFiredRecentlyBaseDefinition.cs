using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class IBehTreeNodeConditionWasEventFiredRecentlyBaseDefinition : CBehTreeNodeConditionDefinition
	{
		[Ordinal(1)] [RED("activationTimeout")] 		public CFloat ActivationTimeout { get; set;}

		[Ordinal(2)] [RED("executionTimeout")] 		public CFloat ExecutionTimeout { get; set;}

		public IBehTreeNodeConditionWasEventFiredRecentlyBaseDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new IBehTreeNodeConditionWasEventFiredRecentlyBaseDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}