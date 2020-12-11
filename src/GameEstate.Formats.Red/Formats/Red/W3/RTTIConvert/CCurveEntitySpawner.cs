using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class CCurveEntitySpawner : CObject
	{
		[Ordinal(1)] [RED("density")] 		public CUInt32 Density { get; set;}

		[Ordinal(2)] [RED("variation")] 		public CFloat Variation { get; set;}

		[Ordinal(3)] [RED("templateWeights", 2,0)] 		public CArray<SEntityWeight> TemplateWeights { get; set;}

		public CCurveEntitySpawner(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static new CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new CCurveEntitySpawner(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}