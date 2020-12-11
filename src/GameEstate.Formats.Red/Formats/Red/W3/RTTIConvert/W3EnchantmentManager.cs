using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3EnchantmentManager : CObject
	{
		[Ordinal(1)] [RED("schematics", 2,0)] 		public CArray<SEnchantmentSchematic> Schematics { get; set;}

		[Ordinal(2)] [RED("craftMasterComp")] 		public CHandle<W3CraftsmanComponent> CraftMasterComp { get; set;}

		[Ordinal(3)] [RED("schematicsNames", 2,0)] 		public CArray<CName> SchematicsNames { get; set;}

		public W3EnchantmentManager(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3EnchantmentManager(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}