using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class newGameConfig : CVariable
	{
		[Ordinal(1)] [RED("tutorialsOn")] 		public CBool TutorialsOn { get; set;}

		[Ordinal(2)] [RED("difficulty")] 		public CInt32 Difficulty { get; set;}

		[Ordinal(3)] [RED("simulate_import")] 		public CBool Simulate_import { get; set;}

		[Ordinal(4)] [RED("import_save_index")] 		public CInt32 Import_save_index { get; set;}

		public newGameConfig(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new newGameConfig(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}