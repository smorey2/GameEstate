using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAnimDangleConstraint_Hinge : CAnimSkeletalDangleConstraint
	{
		[Ordinal(1)] [RED("name")] 		public CString Name { get; set;}

		[Ordinal(2)] [RED("radius")] 		public CFloat Radius { get; set;}

		[Ordinal(3)] [RED("limit")] 		public CFloat Limit { get; set;}

		[Ordinal(4)] [RED("bounce")] 		public CFloat Bounce { get; set;}

		[Ordinal(5)] [RED("damp")] 		public CFloat Damp { get; set;}

		[Ordinal(6)] [RED("min")] 		public CFloat Min { get; set;}

		[Ordinal(7)] [RED("max")] 		public CFloat Max { get; set;}

		[Ordinal(8)] [RED("inertia")] 		public CFloat Inertia { get; set;}

		[Ordinal(9)] [RED("gravity")] 		public CFloat Gravity { get; set;}

		[Ordinal(10)] [RED("spring")] 		public CFloat Spring { get; set;}

		public CAnimDangleConstraint_Hinge(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAnimDangleConstraint_Hinge(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}