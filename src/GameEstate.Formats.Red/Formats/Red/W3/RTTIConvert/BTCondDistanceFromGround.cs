using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class BTCondDistanceFromGround : IBehTreeTask
	{
		[Ordinal(1)] [RED("checkedActor")] 		public CEnum<EStatOwner> CheckedActor { get; set;}

		[Ordinal(2)] [RED("value")] 		public CFloat Value { get; set;}

		[Ordinal(3)] [RED("operator")] 		public CEnum<EOperator> Operator { get; set;}

		[Ordinal(4)] [RED("m_collisionGroupNames", 2,0)] 		public CArray<CName> M_collisionGroupNames { get; set;}

		public BTCondDistanceFromGround(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new BTCondDistanceFromGround(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}