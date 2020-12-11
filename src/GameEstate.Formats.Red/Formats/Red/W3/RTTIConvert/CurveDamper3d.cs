using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CurveDamper3d : CObject
	{
		[Ordinal(1)] [RED("damperX")] 		public CHandle<CurveDamper> DamperX { get; set;}

		[Ordinal(2)] [RED("damperY")] 		public CHandle<CurveDamper> DamperY { get; set;}

		[Ordinal(3)] [RED("damperZ")] 		public CHandle<CurveDamper> DamperZ { get; set;}

		public CurveDamper3d(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CurveDamper3d(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}