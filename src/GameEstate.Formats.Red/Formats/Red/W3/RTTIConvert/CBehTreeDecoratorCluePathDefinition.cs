using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBehTreeDecoratorCluePathDefinition : IBehTreeNodeDecoratorDefinition
	{
		[Ordinal(1)] [RED("clueTemplate")] 		public CHandle<CEntityTemplate> ClueTemplate { get; set;}

		[Ordinal(2)] [RED("clueTemplate_var")] 		public CName ClueTemplate_var { get; set;}

		[Ordinal(3)] [RED("maxClues")] 		public CBehTreeValInt MaxClues { get; set;}

		[Ordinal(4)] [RED("cluesOffset")] 		public CBehTreeValFloat CluesOffset { get; set;}

		public CBehTreeDecoratorCluePathDefinition(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBehTreeDecoratorCluePathDefinition(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}