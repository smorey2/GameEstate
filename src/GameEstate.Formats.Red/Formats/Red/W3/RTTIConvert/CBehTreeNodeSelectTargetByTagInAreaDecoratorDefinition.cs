using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeNodeSelectTargetByTagInAreaDecoratorDefinition : CBehTreeNodeSelectTargetByTagDecoratorDefinition
	{
		[Ordinal(1)] [RED("areaSelection")] 		public SBehTreeAreaSelection AreaSelection { get; set;}

		[Ordinal(2)] [RED("getClosest")] 		public CBehTreeValBool GetClosest { get; set;}

		[Ordinal(3)] [RED("reselectOnActivate")] 		public CBehTreeValBool ReselectOnActivate { get; set;}

		public CBehTreeNodeSelectTargetByTagInAreaDecoratorDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeNodeSelectTargetByTagInAreaDecoratorDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}