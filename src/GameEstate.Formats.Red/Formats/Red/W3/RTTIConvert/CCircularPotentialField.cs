using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CCircularPotentialField : IPotentialField
	{
		[Ordinal(1)] [RED("radius")] 		public CFloat Radius { get; set;}

		[Ordinal(2)] [RED("rangeTop")] 		public CFloat RangeTop { get; set;}

		[Ordinal(3)] [RED("rangeBottom")] 		public CFloat RangeBottom { get; set;}

		[Ordinal(4)] [RED("origin")] 		public Vector Origin { get; set;}

		[Ordinal(5)] [RED("solid")] 		public CBool Solid { get; set;}

		public CCircularPotentialField(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CCircularPotentialField(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}