using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskTauntDef : CBTTaskPlayAnimationEventDecoratorDef
	{
		[Ordinal(1)] [RED("tauntType")] 		public CEnum<ETauntType> TauntType { get; set;}

		[Ordinal(2)] [RED("tauntDelay")] 		public CFloat TauntDelay { get; set;}

		[Ordinal(3)] [RED("useXMLTauntChance")] 		public CBool UseXMLTauntChance { get; set;}

		public CBTTaskTauntDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskTauntDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}