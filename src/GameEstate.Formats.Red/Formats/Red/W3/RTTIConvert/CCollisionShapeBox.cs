using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CCollisionShapeBox : ICollisionShape
	{
		[Ordinal(1)] [RED("physicalMaterialName")] 		public CName PhysicalMaterialName { get; set;}

		[Ordinal(2)] [RED("halfExtendsX")] 		public CFloat HalfExtendsX { get; set;}

		[Ordinal(3)] [RED("halfExtendsY")] 		public CFloat HalfExtendsY { get; set;}

		[Ordinal(4)] [RED("halfExtendsZ")] 		public CFloat HalfExtendsZ { get; set;}

		public CCollisionShapeBox(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CCollisionShapeBox(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}