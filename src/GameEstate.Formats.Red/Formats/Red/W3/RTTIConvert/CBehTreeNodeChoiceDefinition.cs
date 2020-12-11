using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeNodeChoiceDefinition : IBehTreeNodeCompositeDefinition
	{
		[Ordinal(1)] [RED("useScoring")] 		public CBool UseScoring { get; set;}

		[Ordinal(2)] [RED("selectRandom")] 		public CBool SelectRandom { get; set;}

		[Ordinal(3)] [RED("forwardChildrenCompletness")] 		public CBool ForwardChildrenCompletness { get; set;}

		public CBehTreeNodeChoiceDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeNodeChoiceDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}