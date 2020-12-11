using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeNodeConditionalChooseBranchDefinition : IBehTreeMetanodeDefinition
	{
		[Ordinal(1)] [RED("child1")] 		public CPtr<IBehTreeNodeDefinition> Child1 { get; set;}

		[Ordinal(2)] [RED("child2")] 		public CPtr<IBehTreeNodeDefinition> Child2 { get; set;}

		[Ordinal(3)] [RED("val")] 		public CBehTreeValBool Val { get; set;}

		public CBehTreeNodeConditionalChooseBranchDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeNodeConditionalChooseBranchDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}