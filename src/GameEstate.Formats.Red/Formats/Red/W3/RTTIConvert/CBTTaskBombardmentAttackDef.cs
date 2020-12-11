using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CBTTaskBombardmentAttackDef : IBehTreeTaskDefinition
	{
		[Ordinal(1)] [RED("initialDelay")] 		public CFloat InitialDelay { get; set;}

		[Ordinal(2)] [RED("afterSpawnDelay")] 		public CFloat AfterSpawnDelay { get; set;}

		[Ordinal(3)] [RED("yOffset")] 		public CFloat YOffset { get; set;}

		[Ordinal(4)] [RED("fxName")] 		public CName FxName { get; set;}

		public CBTTaskBombardmentAttackDef(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CBTTaskBombardmentAttackDef(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}