using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CAreaTestComponent : CComponent
	{
		[Ordinal(1)] [RED("traceDistance")] 		public CFloat TraceDistance { get; set;}

		[Ordinal(2)] [RED("extents")] 		public Vector Extents { get; set;}

		[Ordinal(3)] [RED("searchRadius")] 		public CFloat SearchRadius { get; set;}

		public CAreaTestComponent(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CAreaTestComponent(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}