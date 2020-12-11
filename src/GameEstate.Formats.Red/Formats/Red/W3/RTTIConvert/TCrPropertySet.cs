using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class TCrPropertySet : CObject
	{
		[Ordinal(1)] [RED("shoulderWeight")] 		public CFloat ShoulderWeight { get; set;}

		[Ordinal(2)] [RED("shoulderLimitUpDeg")] 		public CFloat ShoulderLimitUpDeg { get; set;}

		[Ordinal(3)] [RED("shoulderLimitDownDeg")] 		public CFloat ShoulderLimitDownDeg { get; set;}

		[Ordinal(4)] [RED("shoulderLimitLeftDeg")] 		public CFloat ShoulderLimitLeftDeg { get; set;}

		[Ordinal(5)] [RED("shoulderLimitRightDeg")] 		public CFloat ShoulderLimitRightDeg { get; set;}

		public TCrPropertySet(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new TCrPropertySet(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}