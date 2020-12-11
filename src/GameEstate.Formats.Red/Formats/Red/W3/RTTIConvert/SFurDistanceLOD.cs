using System.IO;
using System.Runtime.Serialization;
using GameEstate.Formats.Red.CR2W.Reflection;
using FastMember;
using static GameEstate.Formats.Red.Records.Enums;


namespace GameEstate.Formats.Red.Types
{
	[DataContract(Namespace = "")]
	[REDMeta]
	public class SFurDistanceLOD : CVariable
	{
		[Ordinal(1)] [RED("enableDistanceLOD")] 		public CBool EnableDistanceLOD { get; set;}

		[Ordinal(2)] [RED("distanceLODStart")] 		public CFloat DistanceLODStart { get; set;}

		[Ordinal(3)] [RED("distanceLODEnd")] 		public CFloat DistanceLODEnd { get; set;}

		[Ordinal(4)] [RED("distanceLODFadeStart")] 		public CFloat DistanceLODFadeStart { get; set;}

		[Ordinal(5)] [RED("distanceLODWidth")] 		public CFloat DistanceLODWidth { get; set;}

		[Ordinal(6)] [RED("distanceLODDensity")] 		public CFloat DistanceLODDensity { get; set;}

		public SFurDistanceLOD(CR2WFile cr2w, CVariable parent, string name) : base(cr2w, parent, name){ }

		public static CVariable Create(CR2WFile cr2w, CVariable parent, string name) => new SFurDistanceLOD(cr2w, parent, name);

		public override void Read(BinaryReader file, uint size) => base.Read(file, size);

		public override void Write(BinaryWriter file) => base.Write(file);

	}
}