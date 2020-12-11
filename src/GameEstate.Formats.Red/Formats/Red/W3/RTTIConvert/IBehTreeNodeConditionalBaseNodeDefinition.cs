using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class IBehTreeNodeConditionalBaseNodeDefinition : IBehTreeMetanodeDefinition
	{
		[Ordinal(1)] [RED("childNodeToDisableCount")] 		public CUInt32 ChildNodeToDisableCount { get; set;}

		[Ordinal(2)] [RED("child")] 		public CPtr<IBehTreeNodeDefinition> Child { get; set;}

		[Ordinal(3)] [RED("invertCondition")] 		public CBool InvertCondition { get; set;}

		public IBehTreeNodeConditionalBaseNodeDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new IBehTreeNodeConditionalBaseNodeDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}