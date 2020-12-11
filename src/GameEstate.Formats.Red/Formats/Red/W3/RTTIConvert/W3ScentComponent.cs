using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class W3ScentComponent : CR4Component
	{
		[Ordinal(1)] [RED("foodGroup")] 		public CEnum<EFoodGroup> FoodGroup { get; set;}

		[Ordinal(2)] [RED("attractionRange")] 		public CFloat AttractionRange { get; set;}

		[Ordinal(3)] [RED("deadAttractionRange")] 		public CFloat DeadAttractionRange { get; set;}

		[Ordinal(4)] [RED("bleedingAttractionRange")] 		public CFloat BleedingAttractionRange { get; set;}

		public W3ScentComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new W3ScentComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}