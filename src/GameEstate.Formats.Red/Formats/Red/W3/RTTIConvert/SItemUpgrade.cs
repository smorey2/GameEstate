using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SItemUpgrade : CVariable
	{
		[Ordinal(1)] [RED("upgradeName")] 		public CName UpgradeName { get; set;}

		[Ordinal(2)] [RED("localizedName")] 		public CName LocalizedName { get; set;}

		[Ordinal(3)] [RED("localizedDescriptionName")] 		public CName LocalizedDescriptionName { get; set;}

		[Ordinal(4)] [RED("cost")] 		public CInt32 Cost { get; set;}

		[Ordinal(5)] [RED("iconPath")] 		public CString IconPath { get; set;}

		[Ordinal(6)] [RED("ability")] 		public CName Ability { get; set;}

		[Ordinal(7)] [RED("ingredients", 2,0)] 		public CArray<SItemParts> Ingredients { get; set;}

		[Ordinal(8)] [RED("requiredUpgrades", 2,0)] 		public CArray<CName> RequiredUpgrades { get; set;}

		public SItemUpgrade(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SItemUpgrade(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}