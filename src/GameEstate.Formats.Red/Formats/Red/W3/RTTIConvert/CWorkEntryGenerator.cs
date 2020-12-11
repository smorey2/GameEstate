using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CWorkEntryGenerator : CSpawnTreeBaseEntryGenerator
	{
		[Ordinal(1)] [RED("entries", 2,0)] 		public CArray<SWorkEntryGeneratorParam> Entries { get; set;}

		[Ordinal(2)] [RED("commonSpawnParams")] 		public SCreatureEntrySpawnerParams CommonSpawnParams { get; set;}

		[Ordinal(3)] [RED("workCategories", 2,0)] 		public CArray<SWorkCetegoriesForCreatureDefinitionEntryGeneratorParam> WorkCategories { get; set;}

		public CWorkEntryGenerator(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CWorkEntryGenerator(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}